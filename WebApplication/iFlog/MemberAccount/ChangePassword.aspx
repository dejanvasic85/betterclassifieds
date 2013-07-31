<%@ Page Title="Change Classies Password" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MemberDetails.master" CodeBehind="ChangePassword.aspx.vb" Inherits="BetterclassifiedsWeb.ChangePassword" %>


<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="memberContentMain">

    <div id="mainHeaderMyAccount">
        <asp:Image ID="imgAccHeader" runat="server" ImageUrl="~/Resources/Images/my_account_header.gif" AlternateText="My Account" />
        <h2>Welcome, 
        <asp:LoginName ID="LoginName1" runat="server" />
            <paramountIt:MessageNotifyControl ID="notifyControl" runat="server" />
        </h2>
        <h3>
            <asp:Label ID="lblHeader" runat="server" Text="Change Password"></asp:Label></h3>
    </div>

    <div id="mainContentMyAccount">

        <div style="margin-left: 10px;">
            <asp:ChangePassword ID="ChangePassword1" runat="server"
                ContinueDestinationPageUrl="~/MemberAccount/MemberDetails.aspx"
                CancelDestinationPageUrl="~/MemberAccount/MemberDetails.aspx">
                <ChangePasswordTemplate>
                    <div id="myAccountTableChgDet">
                        <table width="570" border="0" cellspacing="" cellpadding="8px">
                            <tr>
                                <td width="210">
                                    <p>
                                        Old Password:
                                    </p>
                                </td>
                                <td width="360">
                                    <asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password" size="40%"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server"
                                        ControlToValidate="CurrentPassword" ErrorMessage="Password is required."
                                        ToolTip="Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div id="myAccountTableChgDet">
                        <table width="570" border="0" cellspacing="" cellpadding="8px">
                            <tr>
                                <td width="210">
                                    <p>
                                        New Password:
                                    </p>
                                </td>
                                <td width="360">
                                    <asp:TextBox ID="NewPassword" runat="server" TextMode="Password" size="40%"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server"
                                        ControlToValidate="NewPassword" ErrorMessage="New Password is required."
                                        ToolTip="New Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td width="210">
                                    <p>
                                        Repeat New Password:
                                    </p>
                                </td>
                                <td width="360">
                                    <asp:TextBox ID="ConfirmNewPassword" runat="server" TextMode="Password" size="40%"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server"
                                        ControlToValidate="ConfirmNewPassword"
                                        ErrorMessage="Confirm New Password is required."
                                        ToolTip="Confirm New Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <p>
                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                        <asp:CompareValidator ID="NewPasswordCompare" runat="server"
                                            ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword"
                                            Display="Dynamic"
                                            ErrorMessage="The Confirm New Password must match the New Password entry."
                                            ValidationGroup="ChangePassword1"></asp:CompareValidator>
                                    </p>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div class="accountRow">
                        <div class="btn-group pull-right">
                            <asp:LinkButton ID="CancelPushButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" CssClass="btn btn-warning" />
                            <asp:LinkButton ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword" Text="Update" CssClass="btn btn-default" ValidationGroup="ChangePassword1" />
                        </div>

                    </div>

                </ChangePasswordTemplate>

                <SuccessTemplate>
                    <table border="0" cellpadding="1" cellspacing="0"
                        style="border-collapse: collapse;">
                        <tr>
                            <td>
                                <table border="0" cellpadding="0">
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:Label ID="Label5" runat="server" Text="Change Password Complete"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSuccess" runat="server"
                                                Text="Your password has been changed sucessfully."></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2">
                                            <asp:Button ID="ContinuePushButton" runat="server" CausesValidation="False"
                                                CommandName="Continue" Text="Continue" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </SuccessTemplate>
            </asp:ChangePassword>
        </div>
    </div>
</asp:Content>

