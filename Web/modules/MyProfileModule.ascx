<%@ Control Language="c#" Inherits="RO.Web.MyProfileModule" CodeFile="MyProfileModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
<script type="text/javascript" lang="javascript">
    function slide(e) {
        var elementLeft = jQuery(e).position().left;
        var overlay = null;;
        var dialog = $('#ActionPanel').dialog({
            autoOpen: false,
            resizable: false,
            modal: true,
            open: function (event, ui) { overlay = $('.ui-widget-overlay').css({ background: $.browser.msie && ($.browser.version + 1) < 10 ? 'white' : 'transparent', position: 'fixed' }).click(function () { $('#ActionPanel').dialog("close"); }); $('#ProfileBtn').html('<asp:Image runat="server" ImageUrl="../images/ArrowUp.png" CssClass="Expand" />'); },
            close: function (event, ui) { if (overlay) { overlay.css({ background: '', position: '' }).unbind('click'); }; $('#ProfileBtn').html('<asp:Image runat="server" ImageUrl="../images/ArrowDn.png" CssClass="Expand" />'); }
        }).parent().appendTo("form:first");
        var form = $('section.Form');
        var formWidth = form.width();
        var dialogWidth = dialog.width();
        var x;
        var y = jQuery(e).position().top + 30;

        if (elementLeft * 2 < formWidth) {
            x = form.position().left;
        } else {
            x = $(document).width() - (jQuery(document).width() - formWidth - form.position().left) - dialogWidth;
        }
        $('#ActionPanel').dialog('option', 'position', [x, y]);
        $(".ui-dialog-titlebar").hide();
        var p = $("#ActionPanel");
        if (!p.is(":visible")) {
            $('#ActionPanel').dialog('open');
        } else {
            $('.ui-widget-overlay').css({ background: '' }).unbind('click');
            $('#ActionPanel').dialog('close');
        }
    }
</script>
<div>
    <asp:HyperLink ID="SignIn" CssClass="SignInLink HideOnMobile" Text="Sign In" runat="server" />
    <asp:Panel ID="SignoutPanel" CssClass="r-table" runat="server">
        <div class="ProfileDisp r-tr">
            <div class="r-table">
                <div class="r-tr">
                    <div class="r-td">
                        <a id='ProfileLbl' class="ProfileUser" onclick="slide(this);return false"><asp:Label ID="UserLabel" CssClass="inp-lbl LbUsrName" runat="server" /></a>
                    </div>
                    <div class="r-td">
                        <a id='ProfileBtn' class="ProfileBtn" onclick="slide(this);return false"><asp:Image runat="server" ImageUrl="../images/ArrowDn.png" CssClass="Expand" /></a>
                    </div>
                    <div class="r-td">
                        <asp:Label ID="CurrCmp" CssClass="inp-lbl ProfCmp" runat="server" />
                    </div>
                    <div class="r-td">
                        <asp:Label ID="CurrPrj" CssClass="inp-lbl ProfPrj" runat="server" />
                    </div>
                    <div class="r-td">
                        <asp:Label ID="CurrSys" CssClass="inp-lbl ProfSys" runat="server" />
                    </div>
                    <div class="r-td">
                        <asp:Button ID="SignoutButton2" CssClass="signOutButton small blue button" runat="server" OnClick="SignoutButton_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</div>
<div id="ActionPanel" style="display: none;" class="ProfileBox">
    <div>
        <asp:Image ID="UsrPic" Width="40px" Height="40px" Visible="false" runat="server" CssClass="ThumbNail" />
    </div>
    <div class="PeofileDetail">
        <div>
            <asp:Label ID="CompanyLabel" CssClass="inp-lbl" runat="server" />
        </div>
        <div>
            <asp:DropDownList ID="CompanyList" CssClass="inp-ddl" runat="server" DataTextField="CompanyDesc" DataValueField="CompanyId" AutoPostBack="true" OnSelectedIndexChanged="CompanyList_SelectedIndexChanged" />
        </div>
        <div>
            <asp:Label ID="ProjectLabel" CssClass="inp-lbl" runat="server" />
        </div>
        <div>
            <asp:DropDownList ID="ProjectList" CssClass="inp-ddl" runat="server" DataTextField="ProjectDesc" DataValueField="ProjectId" AutoPostBack="true" OnSelectedIndexChanged="ProjectList_SelectedIndexChanged" />
        </div>
        <div>
            <asp:Label ID="SystemsLabel" CssClass="inp-lbl" runat="server" />
        </div>
        <div>
            <asp:DropDownList ID="SystemsList" CssClass="inp-ddl" runat="server" DataTextField="SystemName" DataValueField="SystemId" AutoPostBack="true" OnSelectedIndexChanged="SystemsList_SelectedIndexChanged" />
        </div>
        <div>
            <asp:DropDownList ID="TimeZoneList" CssClass="inp-ddl" runat="server" DataTextField="TimeZoneName" DataValueField="TimeZoneId" AutoPostBack="true" OnSelectedIndexChanged="TimeZoneList_SelectedIndexChanged" />
        </div>
    </div>
    <div class="ProfileBtnGroup">
        <div style="float: left;">
            <asp:HyperLink ID="cMyAccountLink" runat="server" />
        </div>
        <div style="float: right;">
            <asp:Button ID="SignoutButton1" CssClass="small blue button" runat="server" OnClick="SignoutButton_Click" CausesValidation="false" />
        </div>
    </div>
</div>
