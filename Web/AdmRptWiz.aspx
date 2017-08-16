<%@ Page language="c#" MasterPageFile="Default.master" EnableEventValidation="false" Inherits="RO.Web.AdmRptWiz" CodeFile="AdmRptWiz.aspx.cs" Title="Rintagi - Report Generator" %>
<%@ Register TagPrefix="Module" TagName="AdmRptWiz" Src="modules/AdmRptWizModule.ascx" %>
<asp:Content ContentPlaceHolderID="MHR" Runat="Server"><Module:AdmRptWiz id="ModuleAdmRptWiz" runat="server" /></asp:Content>
