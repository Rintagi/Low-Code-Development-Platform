<%@ Control Language="c#" Inherits="RO.Web.FooterModule" CodeFile="FooterModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
<script>
</script>
<div class="HideBgImgOnMobile PageObj32">
<div class="r-table rg-1-12 SctGrpRow2"><div class="r-tr">
    <div class="r-td rc-1-1 SctGrpCol7">
        <div class="SctGrpDiv7">
        <div class="r-table PageObj11"><div class="r-tr"><div class="r-td">
            <asp:HyperLink Text="Terms of Service" CssClass="PageLnk23" OnClick="SearchLink('http://www.rintagi.com/home/terms_of_service.pdf','','',''); return stopEvent(this,event);" NavigateUrl="#" runat="server" />
            <asp:HyperLink Text="Privacy Policy" CssClass="PageLnk24" OnClick="SearchLink('http://www.rintagi.com/home/privacy_policy.pdf','','',''); return stopEvent(this,event);" NavigateUrl="#" runat="server" />
        </div></div></div>
        </div>
    </div>
    <div class="r-td rc-2-11 SctGrpCol8">
        <div class="SctGrpDiv8">
        <div class="PageObj12">
            <asp:Label id="cVersionTxt" runat="server" />
        </div>
        </div>
    </div>
    <div class="r-td rc-12-12 SctGrpCol9">
        <div class="SctGrpDiv9">
        <div class="r-table PageObj13"><div class="r-tr"><div class="r-td">
            <asp:HyperLink Text="Need help? Get support." CssClass="PageLnk25" OnClick="email('cs','robocoder.com','A'); return false;" runat="server" />
        </div></div></div>
        </div>
    </div>
</div></div>
</div>
