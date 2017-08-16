<%@ Page language="c#" MasterPageFile="Default.master" EnableEventValidation="false" Inherits="RO.Web.AdmRptDbTable" CodeFile="AdmRptDbTable.aspx.cs" Title="Tables and Columns" %>
<%@ Register TagPrefix="Module" TagName="AdmRptDbTable" Src="reports/AdmRptDbTableModule.ascx" %>
<asp:Content ContentPlaceHolderID="MHR" Runat="Server"><Module:AdmRptDbTable id="M2" runat="server" /></asp:Content>
