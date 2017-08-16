<%@ Control Language="c#" Inherits="RO.Web.ComProgramModule" CodeFile="ComProgramModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
<script type="text/javascript" lang="javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginRequestHandler)
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandler)
    function beginRequestHandler() { ShowProgress(); document.body.style.cursor = 'wait'; }
    function endRequestHandler() { HideProgress(); document.body.style.cursor = 'auto'; }
</script>
<div id="AjaxSpinner" class="AjaxSpinner" style="display:none;">
	<div style="padding:10px;">
		<img alt="" src="images/indicator.gif" />&nbsp;<asp:Label ID="AjaxSpinnerLabel" Text="This may take a moment..." runat="server" />
	</div>
</div>
<asp:UpdatePanel UpdateMode="Conditional" runat="server"><ContentTemplate>
<div class="r-table wizard-header">
<div class="r-tr">
    <div class="r-td rc-1-6">
        <div class="wizard-title"><asp:label id="cTitleLabel" runat="server" /></div>
    </div>
    <div class="r-td rc-7-12">
        <div class="wizard-entity">
            <div class="r-table">
                <div class="r-tr">
                    <div class="r-td r-labelR"><asp:Label ID="Label2" CssClass="inp-lbl" runat="server" text="Entity:" /></div>&nbsp;
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
            <div style="float:right;"><img src="./images/wizard/compile-dsk.jpg" class="wizard-image-dsk" style="max-width:200px" /></div>
            <div style="float:none;"><img src="./images/wizard/compile-mob.jpg" class="wizard-image-mob" style="max-width:500px" /></div>
            <div style="clear:both;"></div>
        </div>
    </div>
    <div class="r-td rc-7-12">
        <div class="wizard-content">
			<asp:TextBox id="cCompileMsg" TextMode="MultiLine" CssClass="inp-txt" AutoPostBack="false" style="height:260px" runat="server" />
        </div>
    </div>
</div>
</div>
<div class="r-table">
<div class="r-tr">
    <div class="r-td rc-1-12">
        <div class="wizard-action">
		    <span style="text-align:right;">
			    <asp:label cssclass="MsgText" id="cMsgLabel" runat="server" />
			    <asp:Button id="cGenButton" CssClass="small blue button" onClick="cGenButton_Click" runat="server" text="Compile" tooltip="Click here to compile all programs for the selected project." />
		    </span>
        </div>
    </div>
</div>
</div>
<asp:Label ID="cMsgContent" runat="server" style="display:none;" EnableViewState="false"/>
</ContentTemplate></asp:UpdatePanel>
