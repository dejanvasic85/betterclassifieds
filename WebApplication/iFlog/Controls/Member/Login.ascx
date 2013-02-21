<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Login.ascx.vb" Inherits="BetterclassifiedsWeb.Login" %>
<asp:Login ID="Login1" runat="server" PasswordRecoveryUrl="~/GetPassword.aspx">
    <LayoutTemplate>
        <asp:Panel ID="pnllogin" DefaultButton="btnLogin">
        <div id="myAccountTableChgDet">
            <table width="570" border="0" align="center" cellpadding="3px" cellspacing="0px">
                <tr>
                    <td width="190"></td>
                    <td width="100">
                        <p>
                            Username:</p>
                    </td>
                    <td width="150">
                        <asp:TextBox ID="UserName" runat="server" Width="150px"></asp:TextBox>
                    </td>
                    <td width="130">
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                            ControlToValidate="UserName" ErrorMessage="Username is required"
                            ToolTip="Username is required" ValidationGroup="ctl00$Login1">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="190"></td>
                    <td width="100"><p>Password:</p></td>
                    <td width="150">
                        <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                    </td>
                    <td width="130">
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                            ControlToValidate="Password" ErrorMessage="Password is required"
                            ToolTip="Password is required" ValidationGroup="ctl00$Login1">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="190"></td>
                    <td width="100"></td>
                    <td width="150">
                        <h6>
                            <asp:HyperLink ID="HyperLink2" runat="server" Text="Forgot password?" NavigateUrl="~/GetPassword.aspx"></asp:HyperLink></h6>
                    </td>
                    <td width="260"></td>
                </tr>
                <tr>
                    <td width="190"></td>
                    <td colspan="2" style="color:Red;">
                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                    </td>
                    <td width="260"></td>
                </tr>
            </table>
        </div>
    
        <div id="myAccountTableChgDet">
            <table width="570" border="0" align="center" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td width="190"></td>
                    <td width="250">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/Images/red_arrow_small_right.gif" AlternateText="red arrow" />
                    
                        <h5>
                            <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time" />.</h5>
                    </td>
                    <td width="130"></td>
                </tr>
            </table>
        </div>
   
        <div id="myAccountTableButtonsLogin">
            <ul>
                <div id="myAccountTableButtonsExtend"><%--
                    <li><asp:LinkButton ID="LoginButton" runat="server" 
                            CommandName="Login" Text="LOGIN" ValidationGroup="ctl00$Login1" /></li>--%>
                    <li><asp:Button ID="btnLogin" runat="server" 
                            CommandName="Login" Text="Login" ValidationGroup="ctl00$Login1" /></li>
                </div>
            </ul>
        </div>
        </asp:Panel>
    </LayoutTemplate>
</asp:Login>
