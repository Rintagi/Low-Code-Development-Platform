<%@ Page language="c#" Inherits="RO.Web.ViewChart" CodeFile="ViewChart.aspx.cs" CodeFileBaseClass="RO.Web.PageBase" EnableEventValidation="false" %>
<%@ Register TagPrefix="Module" TagName="ViewChart" Src="modules/ViewChartModule.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<link rel='stylesheet' type='text/css' href='css/flowchart.css'>

<script type='text/javascript' src='scripts/raphael.js?version=1.1.1'></script>
<script type='text/javascript' src='scripts/jquery-1.11.0.min.js'></script>
<script type='text/javascript' src='scripts/jquery-ui-1.10.4.custom.min.js'></script>
<script type='text/javascript' src='scripts/flowchart.js?version=1.6.5'></script>
<script type='text/javascript' src='https://d3js.org/d3.v4.js'></script>
</head>
<body>
        <form id="ViewChartForm" method="post" runat="server">
			<Module:ViewChart id="ModuleViewChart" runat="server" />
		</form>
</body>
</html>
