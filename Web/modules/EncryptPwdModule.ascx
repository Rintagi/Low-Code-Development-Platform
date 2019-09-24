<%@ Control Language="c#" Inherits="RO.Web.EncryptPwdModule" CodeFile="EncryptPwdModule.ascx.cs" CodeFileBaseClass="RO.Web.ModuleBase" %>
<%@ Register TagPrefix="rcasp" NameSpace="RoboCoder.WebControls" Assembly="WebControls, Culture=neutral" %>
<div class="r-table BannerGrp">
<div class="r-tr">
    <div class="rc-1-4">
        <div class="BannerNam">
		    <asp:label CssClass="screen-title" Text="Encrypt Password" runat="server" /><input type="image" name="cDefaultFocus" id="cClientFocusButton" src="images/Help_x.jpg" onclick="return false;" style="visibility:hidden;" />
        </div>
    </div>
</div>
</div>
<asp:Panel id="cTabFolder" CssClass="TabFolder" runat="server">
<div class="r-table rg-1-9">
<div class="r-tr">
    <div class="rc-1-8">
        <div class="screen-tabfolder">
            <div class="r-table">
                <div class="r-tr">
                <div class="r-labelR">
        			<asp:Label id="cInstrLabel" CssClass="inp-lbl" runat="server" text="Before Encryption:" />
                </div>
                <div class="r-content">
        			<asp:textbox id="cInstr" CssClass="inp-txt" runat="server" />
                </div>
                </div>
                <div class="r-tr">
                <div class="r-labelR">
        			<asp:Label id="cOutstrLabel" CssClass="inp-lbl" runat="server" text="After Encryption:" />
                </div>
                <div class="r-content">
        			<asp:textbox ID="cOutstr" CssClass="inp-txt" runat="server" />
                </div>
                </div>
            </div>
        </div>
    </div>
    <div class="rc-9-9">
        <div class="screen-tabfolder">
            <div class="r-table">
                <div class="r-tr">
                <div class="r-content">
        			<asp:Button id="cEncryptButton" CssClass="medium blue button" onclick="cEncryptButton_Click" runat="server" text="Encrypt" />
                    <asp:checkbox id="cValidate" runat="server" Checked="false" Text="test connection" />
                </div>
                </div>
            </div>
        </div>
    </div>
</div>
</div>
</asp:Panel>
<asp:label CssClass="FootText" runat="server" text="Please enter the new sql-server login password, press the Encrypt button to obtain the encrypted string to be copied and pasted to Web.config's <key=DesPassword>. Make sure it is consistent with the actual Database Server password before starting this system again." />
