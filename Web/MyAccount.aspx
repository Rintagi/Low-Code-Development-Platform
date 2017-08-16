<%@ Page language="c#" MasterPageFile="Default.master" EnableEventValidation="false" Inherits="RO.Web.MyAccount" CodeFile="MyAccount.aspx.cs" Title="Rintagi - User Account" %>
<%@ Register TagPrefix="Module" TagName="MyAccount" Src="modules/MyAccountModule.ascx" %>
<asp:Content ContentPlaceHolderID="MHR" Runat="Server">
    <Module:MyAccount id="ModuleMyAccount" runat="server" />
    <asp:Label ID="cMsgContent" runat="server" style="display:none;" EnableViewState="false"/>
</asp:Content>
