<%@ Control Language="c#" Inherits="RO.Web.GenScreensModule" CodeFile="GenScreensModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
<script type="text/javascript" lang="javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginRequestHandler)
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandler)
    function beginRequestHandler() { ShowProgress(); document.body.style.cursor = 'wait'; }
    function endRequestHandler() { HideProgress(); document.body.style.cursor = 'auto'; }
</script>
<div id="AjaxSpinner" class="AjaxSpinner" style="display: none;">
    <div style="padding: 10px;">
        <img alt="" src="images/indicator.gif" />&nbsp;<asp:Label ID="AjaxSpinnerLabel" Text="This may take a moment ..." runat="server" />
    </div>
</div>
<asp:UpdatePanel UpdateMode="Conditional" runat="server">
    <ContentTemplate>
        <div class="r-table wizard-header">
            <div class="r-tr">
                <div class="r-td rc-1-6">
                    <div class="wizard-title">
                        <asp:Label ID="cTitleLabel" runat="server" />
                    </div>
                </div>
                <div class="r-td rc-7-12">
                    <div class="wizard-entity">
                        <div class="r-table">
                            <div class="r-tr">
                                <div class="r-td r-labelR">
                                    <asp:Label CssClass="inp-lbl" runat="server" Text="Entity:" /></div>
                                &nbsp;
                    <div class="r-td r-content">
                        <asp:DropDownList ID="cEntityId" CssClass="inp-ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cEntityId_SelectedIndexChanged" DataValueField="EntityId" DataTextField="EntityName" /></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="r-table">
            <div class="r-tr">
                <div class="r-td rc-1-12">
                    <div class="wizard-help">
                        <asp:Label ID="cHelpLabel" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <div class="r-table">
            <div class="r-tr">
                <div class="r-td rc-1-6">
                    <div class="wizard-image">
                        <div style="float: right;">
                            <img src="./images/wizard/screen-dsk.jpg" class="wizard-image-dsk" style="max-width: 200px" /></div>
                        <div style="float: none;">
                            <img src="./images/wizard/screen-mob.jpg" class="wizard-image-mob" style="max-width: 500px" /></div>
                        <div style="clear: both;"></div>
                    </div>
                </div>
                <div class="r-td rc-7-12">
                    <div class="wizard-content">
                        <table cellspacing="0px" cellpadding="0px" border="0px" width="100%">
                            <tr>
                                <td>
                                    <asp:Label CssClass="inp-lbl" runat="server" Text="Client Tier:" /></td>
                                <td colspan="3">
                                    <asp:DropDownList ID="cClientTierId" CssClass="inp-ddl" runat="server" OnSelectedIndexChanged="cClientTierId_SelectedIndexChanged" AutoPostBack="true" DataValueField="ClientTierId" DataTextField="ClientTierName" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="inp-lbl" runat="server" Text="Rule Tier:" /></td>
                                <td colspan="3">
                                    <asp:DropDownList ID="cRuleTierId" CssClass="inp-ddl" runat="server" OnSelectedIndexChanged="cRuleTierId_SelectedIndexChanged" AutoPostBack="true" DataValueField="RuleTierId" DataTextField="RuleTierName" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="inp-lbl" runat="server" Text="Data Tier:" /></td>
                                <td colspan="3">
                                    <asp:DropDownList ID="cDataTierId" CssClass="inp-ddl" runat="server" OnSelectedIndexChanged="cDataTierId_SelectedIndexChanged" AutoPostBack="true" DataValueField="DataTierId" DataTextField="DataTierName" /></td>
                            </tr>
                            <tr>
                                <td colspan="4" height="10px">
                                    <hr>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="inp-lbl" runat="server" Text="Database:" /></td>
                                <td>
                                    <asp:DropDownList ID="cSystemId" CssClass="inp-ddl" AutoPostBack="true" OnSelectedIndexChanged="cSystemId_SelectedIndexChanged" DataValueField="SystemId" DataTextField="SystemName" runat="server" /></td>
                                <td align="right">
                                    <asp:Label CssClass="inp-lbl" runat="server" Text="All Screens:" /></td>
                                <td width="20px">
                                    <asp:CheckBox ID="cAllScreen" CssClass="inp-chk" AutoPostBack="true" OnCheckedChanged="cAllScreen_CheckedChanged" ToolTip="Check here for all screens in the listbox below to be generated." runat="server" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="inp-lbl" runat="server" Text="Abbreviation:" /></td>
                                <td>
                                    <asp:Label ID="cAbbreviation" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="inp-lbl" runat="server" Text="Search:" /></td>
                                <td colspan="2">
                                    <asp:TextBox ID="cSearch" CssClass="inp-txt" runat="server" MaxLength="25" /></td>
                                <td>
                                    <asp:ImageButton ID="cSearchButton" ImageUrl="~/images/Search.gif" OnClick="cSearchButton_Click" AlternateText="Click here to search for screens with specified text on the left." runat="server" />
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:ListBox ID="cScreenList" runat="server" CssClass="inp-pic" SelectionMode="Multiple" AutoPostBack="false" Rows="6" DataValueField="ScreenId" DataTextField="ScreenTitle" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="r-table">
            <div class="r-tr">
                <div class="r-td rc-1-12">
                    <div class="wizard-action">
                        <span style="text-align: right;">
                            <asp:Label CssClass="MsgText" ID="cMsgLabel" runat="server" />
                            <asp:Button ID="cCloneButton" CssClass="small blue button" OnClick="cCloneButton_Click" runat="server" Text="Clone" ToolTip="Click here to clone codes for selected screen." />
                            <asp:Button ID="cGenButton" CssClass="small blue button" OnClick="cGenButton_Click" style="min-width: 140px;" runat="server" Text="1. Create" ToolTip="Click here to generate codes for selected screen(s)." />
                             <asp:Button ID="cGenReactButton" CssClass="small blue button" OnClick="cGenReactButton_Click" style="min-width: 140px;" runat="server" Text="2. Create React Screens" ToolTip="Click here to generate ReactJs codes for selected screen(s)." />
                        </span>
                    </div>
                    <div class="wizard-footer">
                        <span>Note: When ready for testing, <a href="ComProgram.aspx?csy=3&id=257">click here to compile all programs</a> once.</span>
                    </div>
                </div>
            </div>
        </div>
        <input id="bErrNow" type="hidden" runat="server" />
        <input id="bInfoNow" type="hidden" runat="server" />
        <asp:Label ID="cMsgContent" runat="server" Style="display: none;" EnableViewState="false" />
    </ContentTemplate>
</asp:UpdatePanel>
