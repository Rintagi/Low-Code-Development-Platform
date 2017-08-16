<%@ Page language="c#" Inherits="RO.Web.GetCss" CodeFile="GetCss.aspx.cs" CodeFileBaseClass="RO.Web.PageBase" EnableEventValidation="false" %>
<%@ Register TagPrefix="Module" TagName="UsrPref" Src="modules/UsrPrefModule.ascx" %>
<!doctype html><html>
	<body>
		<form id="GetCssForm" method="post" runat="server">
			<Module:UsrPref id="ModuleUsrPref" Runat="server" />
		</form>
	</body>
</html>