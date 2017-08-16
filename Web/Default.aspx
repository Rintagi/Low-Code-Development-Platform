<%@ Page Language="C#" MasterPageFile="Default.master" EnableEventValidation="false" CodeFile="Default.aspx.cs" Inherits="RO.Web.Default" %>
<%@ Register TagPrefix="Module" TagName="Default" Src="modules/DefaultModule.ascx" %>
<asp:Content ContentPlaceHolderID="MHR" Runat="Server">
    <Module:Default id="ModuleDefault" runat="server" />
    <asp:Label ID="cMsgContent" runat="server" style="display:none;" EnableViewState="false"/>
</asp:Content>
