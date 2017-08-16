<%@ Control Language="c#" Inherits="RO.Web.HelpModule" CodeFile="HelpModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
<script type="text/javascript">
    Sys.Application.add_load(function () {
        var vpWidth = $(window).width();
        var smallphone = vpWidth <= 400;
        var msg = $('#' + '<%= cHelpTxt.ClientID %>').html();
        var title = $('#' + '<%= cTitle.ClientID %>').html();
        if (smallphone) {
            $('.helpful-tip').on('click', function (e) { PopDialog(null, msg, null, null); });
        }
        else {
            $('.helpful-tip')
              .removeData('qtip')
              .qtip({
                  content: {
                      //              text: 'Please select the appropriate screen column, edit the properties or create new columns, click the Save Button to make the change permanent. Default sorting is on ref column of dropdown; Descending is assumed if sort column is the same as ref.',
                      text: msg,
                      title: {
                          text: title,
                          button: true
                      }
                  },
                  position: {
                      my: 'right top', // Use the corner...
                      at: 'left center' // ...and opposite corner
                  },
                  show: {
                      event: 'click',
                      ready: false // do not show tooltip on page load
                  },
                  hide: {
                      event: 'click'
                  },
                  style: {
                      classes: 'ui-tooltip-shadow ui-tooltip-plain'
                  }
              });
        }
    });
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <div class="helpCnt">
        <div><asp:Button ID="cImage" runat="server" CssClass="helpful-tip" OnClientClick="return false;" alt="Helpful Tip" /></div>
        <asp:Label ID="cHelpTxt" runat="server" Style="display: none"></asp:Label>
        <asp:Label ID="cTitle" runat="server" Style="display: none"></asp:Label>
    </div>
</ContentTemplate>
</asp:UpdatePanel>
