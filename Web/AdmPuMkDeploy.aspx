<%@ Page language="c#" Inherits="RO.Web.AdmPuMkDeploy" CodeFile="AdmPuMkDeploy.aspx.cs" CodeFileBaseClass="RO.Web.PageBase" EnableEventValidation="false" %>
<%@ Register TagPrefix="Module" TagName="AdmPuMkDeploy" Src="modules/AdmPuMkDeployModule.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Make Deployment</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript (ECMAScript)" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="GetCss.aspx" type="text/css" rel="Stylesheet">
	</head>
	<body leftmargin="0" topmargin="0">
		<form id="AdmPuMkDeployForm" method="post" runat="server">
			<Module:AdmPuMkDeploy id="ModuleAdmPuMkDeploy" runat="server" />
			<iframe src="InfoPage.aspx" Width="0" Height="0" Frameborder="0"></iframe>
		</form>
	</body>
</html>