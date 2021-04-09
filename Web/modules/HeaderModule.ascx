<%@ Control Language="c#" Inherits="RO.Web.HeaderModule" CodeFile="HeaderModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
<%@ Register TagPrefix="rcasp" Namespace="RoboCoder.WebControls" Assembly="WebControls, Culture=neutral" %>
<%@ Register TagPrefix="Module" TagName="MenuTop" Src="MenuTopModule.ascx" %>
<%@ Register TagPrefix="Module" TagName="Profile" Src="MyProfileModule.ascx" %>
<script>
    function openLinkSec() { var linkContainer = $('#<%=cLinkHolder.ClientID%>'); if (linkContainer.hasClass('hideMoreButtonSec')) { linkContainer.removeClass('hideMoreButtonSec'); } else { linkContainer.addClass('hideMoreButtonSec'); }; }
    $(document).mouseup(function (e) { var linkContainer = $('#<%=cLinkHolder.ClientID%>'); if ($(window).width() <= 1024) { if (!linkContainer.is(e.target) && linkContainer.has(e.target).length === 0 && !linkContainer.hasClass('hideMoreButtonSec')) { openLinkSec(); } } });
    function openSociSec() { var socialContainer = $('#<%=cSociHolder.ClientID%>'); if (socialContainer.hasClass('hideMoreButtonSec')) { socialContainer.removeClass('hideMoreButtonSec'); } else { socialContainer.addClass('hideMoreButtonSec'); }; }
    $(document).mouseup(function (e) { var socialContainer = $('#<%=cSociHolder.ClientID%>'); if ($(window).width() <= 1024) { if (!socialContainer.is(e.target) && socialContainer.has(e.target).length === 0 && !socialContainer.hasClass('hideMoreButtonSec')) { openSociSec(); } } });
    Sys.Application.add_load(function () { var pid = $('#<%=PageLnk19Img.ClientID%>');
        pid.mouseover(function () { pid.attr('src','images/shortcut/embedr.gif'); });
        pid.mouseout(function () { pid.attr('src','images/shortcut/embed.gif'); });
    });
</script>
<div class="mobileHeader">
    <div class="mobileLogo"><asp:HyperLink NavigateUrl="~/Default.aspx" runat="server"><asp:Image ImageUrl="~/images/special/Robocoder.png" runat="server" /></asp:HyperLink></div>
    <div class="crumbSec"><asp:HyperLink ID="cMobileCrumb" CssClass="crumbMobile" runat="server" /></div>
    <div><section id="mobileMenu"><div class="sb-navbar sb-slide"><div class="sb-toggle-right">
        <asp:Label ID="mMenuTitle" CssClass="mMenuTitle" Text="MENU" runat="server"></asp:Label>
        <div class="navicon-line"></div><div class="navicon-line"></div><div class="navicon-line"></div>
    </div></div></section></div>
    <div><asp:HyperLink ID="cSignIn" CssClass="SignInMobile" Text="Sign In" runat="server" /></div>
    <div><asp:Button ID="cProfileButton" CssClass="ProfBtnSec" OnClientClick="slide(this); return false" Text="Profile" runat="server" /></div>
    <div><asp:Button ID="cLinkButton" CssClass="LinkBtnSec" OnClientClick="openLinkSec(); return false;" Text="Link" runat="server" /></div>
    <div><asp:Button ID="cSociButton" CssClass="SociBtnSec" OnClientClick="openSociSec(); return false;" Text="Social" runat="server" /></div>
</div>
<div class="r-table rg-1-4 SctGrpRow7"><div class="r-tr">
    <div class="r-td rc-1-4 SctGrpCol19 HideOnMobile">
        <div ID="cLogoHolder" class="SctGrpDiv19" runat="server">
        <div class="PageObj1">
            <asp:HyperLink NavigateUrl="~/Default.aspx" runat="server"><asp:Image ID="headerLogo" CssClass="PageLnk35" ImageUrl="~/images/special/YourLogo.gif" runat="server" /></asp:HyperLink>
        </div>
        </div>
    </div>
</div></div>
<div class="r-table rg-5-12 SctGrpRow5"><div class="r-tr">
    <div class="r-td rc-1-11 SctGrpCol20">
        <div ID="cLinkHolder" class="link-content hideMoreButtonSec SctGrpDiv20" runat="server">
        <div class="r-table PageObj3"><div class="r-tr"><div class="r-td">
            <asp:ImageButton ID="PageLnk19Img" ImageUrl="~/images/shortcut/embed.gif" CssClass="PageLnk19" OnClientClick="ShowEmbedScript(); return false;" runat="server" />
            <asp:HyperLink Text="Google" CssClass="PageLnk28" OnClick="SearchLink('http://www.google.com','','',''); return stopEvent(this,event);" NavigateUrl="#" runat="server" />
            <asp:HyperLink Text="SIGN UP" CssClass="PageLnk29" NavigateUrl="tel:AdmSignup.aspx?csy=3" runat="server" />
        </div></div></div>
        </div>
    </div>
    <div class="r-td rc-12-12 SctGrpCol12">
        <div class="SctGrpDiv12">
        <div class="PageObj2">
            <Module:Profile ID="ModuleProfile" runat="server" />
        </div>
        </div>
    </div>
</div></div>
<div class="r-table rg-5-12 SctGrpRow5"><div class="r-tr">
    <div class="r-td rc-1-12 SctGrpCol15">
        <div ID="cSociHolder" class="link-content hideMoreButtonSec SctGrpDiv15" runat="server">
        <div class="r-table PageObj35"><div class="r-tr"><div class="r-td">
            <asp:ImageButton ID="PageLnk60Img" ImageUrl="~/images/special/Facebook.png" CssClass="PageLnk60" runat="server" />
            <asp:ImageButton ID="PageLnk61Img" ImageUrl="~/images/special/Twitter.png" CssClass="PageLnk61" runat="server" />
            <asp:ImageButton ID="PageLnk62Img" ImageUrl="~/images/special/Linkedin.png" CssClass="PageLnk62" runat="server" />
        </div></div></div>
        </div>
    </div>
</div></div>
<div class="r-table rg-1-12 SctGrpRow6"><div class="r-tr">
    <div class="r-td rc-1-12 SctGrpCol15">
        <div class="SctGrpDiv15">
        <div class="PageObj5">
            <Module:MenuTop ID="ModuleMenuTop" runat="server" />
        </div>
        </div>
    </div>
</div></div>
<div class="r-table rg-1-12 SctGrpRow6"><div class="r-tr">
    <div class="r-td rc-1-2 SctGrpCol16 HideOnMobile">
        <div class="SctGrpDiv16">
        <div class="PageObj6">
            <asp:Literal ID="cBreadCrumb" EnableViewState="false" runat="server" />
        </div>
        </div>
    </div>
    <div class="r-td rc-3-8 SctGrpCol17 HideOnMobile">
        <div class="SctGrpDiv17">
        <div class="PageObj7">
            <asp:Label ID="cWelcomeTime" runat="server" />
        </div>
        </div>
    </div>
    <div class="r-td rc-9-12 SctGrpCol18">
        <div class="reset-lang SctGrpDiv18">
        <div class="PageObj8">
            <rcasp:ComboBox ID="cLang" CssClass="inp-ddl" Mode="A" AutoPostBack="true" OnPostBack="cbPostBack" OnSearch="cbCultureId" DataValueField="CultureTypeId" DataTextField="CultureTypeLabel" runat="server" OnSelectedIndexChanged="cLang_SelectedIndexChanged" />
            <asp:ImageButton ID="lanResetBtn" CssClass="lanResetBtn" runat="server" ImageUrl="~/images/reset.jpg" OnClick="lanResetBtn_Click" ToolTip="Reset Language" CausesValidation="false" />
        </div>
        </div>
    </div>
</div></div>
