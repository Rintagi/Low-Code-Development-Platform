<%@ Control Language="c#" Inherits="RO.Web.ComInstallModule" CodeFile="ComInstallModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
<script type="text/javascript" lang="javascript">
    var started = false;
    var cancelled = false;

    function PrepareRelease() { if (!started) { PrePrepare(); RunReleaseR(0); } }
    
    function CancelRelease() { cancelled = true; }

    //initial variables, etc. before release.
    function PrePrepare() {
        cancelled = false;
        started = true;
        document.getElementById('<%=cCompileMsg.ClientID %>').value = '';
        for (var index = 0; index < ids.length; ++index) {
            hideElement(document.getElementById('Complete' + ids[index]));
        }
		hideElement(document.getElementById('<%=cPrepare.ClientID %>'));
		showElement(document.getElementById('<%=cCancel.ClientID %>'));
    }

    //after we have done the releases..
    function PostPrepare() {
        started = false;
		showElement(document.getElementById('<%=cPrepare.ClientID %>'));
		hideElement(document.getElementById('<%=cCancel.ClientID %>'));
    }

    function RunReleaseR(idx) {
        var id = ids[idx];
        var ajax_url = urls[idx];
        var checked = $('#chkRelease' + id).is(':checked');
        if (checked) {
            $('#InProgress' + id).show();
            //prevent caching..
            var date = new Date();
            ajax_url = ajax_url + '&date=' + date.getTime();
            $.ajax({ url: ajax_url, success: function (responseText) {
                document.getElementById('<%=cCompileMsg.ClientID %>').value += responseText;
                hideElement(document.getElementById('InProgress' + id));
                showElement(document.getElementById('Complete' + id));

                //call again if we have more to do..
                //var next = idx + 1
                var next = GetNextRelease(idx);
                if (next < urls.length) {
                    if (!cancelled) { RunReleaseR(next); } else { CancelRemaining(next); }
                }
                PostPrepare();
            }
            });
        }
        else {
            var next = GetNextRelease(idx);
            if (next < urls.length) {
                if (!cancelled) { RunReleaseR(next); }
                else { CancelRemaining(next); }
            }
        }
    }

    //find next checked release..
    function GetNextRelease(idx) {
        var next = idx + 1;
        var id = null;    
        while (next < urls.length) {
            id = ids[next];
            if (document.getElementById('chkRelease' + id).checked) { break; } else { next++; }
        }
        return next;
    }

    function CancelRemaining(idx) {
        var id = null;
        for (var index = idx; index < ids.length; ++index) {
            id = ids[index];
            if (document.getElementById('chkRelease' + id).checked) { showElement(document.getElementById('Cancelled' + id)); }
        }
    }
</script>
<asp:PlaceHolder ID="cPlaceholder" runat="server" />
<%--
Do not add the following unless the special scripts above has been investigated:
<asp:UpdatePanel UpdateMode="Conditional" runat="server"><Triggers><asp:PostBackTrigger ControlID="cLoadButton" /></Triggers><ContentTemplate>
--%>
<div class="r-table wizard-header">
<div class="r-tr">
    <div class="r-td rc-1-6">
        <div class="wizard-title"><asp:label id="cTitleLabel" runat="server" /></div>
    </div>
    <div class="r-td rc-7-12">
        <div class="wizard-entity">
            <div class="r-table">
                <div class="r-tr">
                    <div class="r-td r-labelR"><asp:Label CssClass="inp-lbl" runat="server" text="Entity:" /></div>&nbsp;
                    <div class="r-td r-content"><asp:DropDownList id="cEntityId" CssClass="inp-ddl" runat="server" AutoPostBack="true" onSelectedIndexChanged="cEntityId_SelectedIndexChanged" DataValueField="EntityId" DataTextField="EntityName" /></div>
                </div>
            </div>
        </div>
    </div>
</div>
</div>
<div class="r-table">
<div class="r-tr">
    <div class="r-td rc-1-12">
        <div class="wizard-help"><asp:label id="cHelpLabel" runat="server" /></div>
    </div>
</div>
</div>
<div class="r-table">
<div class="r-tr">
    <div class="r-td rc-1-6">
        <div class="wizard-image">
            <div style="float:right;"><img src="./images/wizard/install-dsk.jpg" class="wizard-image-dsk" style="max-width:200px" /></div>
            <div style="float:none;"><img src="./images/wizard/install-mob.jpg" class="wizard-image-mob" style="max-width:500px" /></div>
            <div style="clear:both;"></div>
        </div>
    </div>
    <div class="r-td rc-7-12">
        <div class="wizard-content">
		<table cellspacing="0px" cellpadding="10px" border="0px" width="100%">
		<tr>
			<td><asp:TextBox id="cCompileMsg" TextMode="MultiLine" CssClass="inp-txt" AutoPostBack="false" style="height:180px" runat="server" /></td>
		</tr>
		<tr>
			<td><asp:Panel ID="cFormPanel" runat="server" /></td>
		</tr>
		</table>
        </div>
    </div>
</div>
</div>
<div class="r-table">
<div class="r-tr">
    <div class="r-td rc-1-6">
        <asp:HyperLink ID="cRegisterLink" runat="server" NavigateUrl="#" Visible="false">Click here to register and acquire a license to unlock this feature</asp:HyperLink>
        <asp:Label ID="cInstallIDLabel" runat="server" Text="Installation ID:" Visible="false"></asp:Label>
        <asp:Label ID="cInstallID" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="cAppIDLabel" runat="server" Text="App ID:" Visible="false"></asp:Label>
        <asp:Label ID="cAppID" runat="server" Text="" Visible="false"></asp:Label>
    </div>
    <div class="r-td rc-7-12">
        <div class="wizard-action">
		    <span style="text-align:right;">
			    <asp:label cssclass="MsgText" id="cMsgLabel" runat="server" />
		        <asp:Button id="cCancel" CssClass="small blue button" runat="server" text="Cancel" OnClientClick="CancelRelease(); return false;" ToolTip="Click here to cancel the remaining generation. Please be patient as the current one cannot be cancelled." style="display:none" />
			    <asp:Button id="cPrepare" CssClass="small blue button" runat="server" text="Prepare" OnClientClick="PrepareRelease(); return false;" ToolTip="Click here to generate the content for the installer." />
			    <asp:Button id="cOkButton" CssClass="small blue button" onClick="cOkButton_Click" runat="server" text="Compile" tooltip="Click here to compile the installer for the selected project." />
			    <asp:Button id="cLoadButton" CssClass="small blue button" onClick="cLoadButton_Click" runat="server" text="Download" tooltip="Click here to download the compiled installer for the selected project." />
		    </span>
        </div>
    </div>
</div>
</div>
<asp:Label ID="cMsgContent" runat="server" style="display:none;" EnableViewState="false"/>
