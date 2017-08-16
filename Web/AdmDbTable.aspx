<%@ Page language="c#" MasterPageFile="Default.master" EnableEventValidation="false" ValidateRequest="false" Inherits="RO.Web.AdmDbTable" CodeFile="AdmDbTable.aspx.cs" Title="Rintagi - Data Table and Columns" %>
<%@ Register TagPrefix="Module" TagName="AdmDbTable" Src="modules/AdmDbTableModule.ascx" %>
<asp:Content ContentPlaceHolderID="MHR" Runat="Server"><Module:AdmDbTable id="M2" runat="server" /></asp:Content>
