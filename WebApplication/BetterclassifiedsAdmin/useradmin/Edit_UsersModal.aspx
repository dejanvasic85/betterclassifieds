<%@ Page Title="" Language="vb" AutoEventWireup="false"  CodeBehind="Edit_UsersModal.aspx.vb" Inherits="BetterclassifiedAdmin.UserAdmin.Edit_UsersModal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Your User Details</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 20px;">
    
    <%-- ajax script manager --%>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
        <%-- ajax update panel start --%>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <%-- ajax tab container start --%>
                <cc1:TabContainer ID="tcntUserInfo" runat="server" ActiveTabIndex="0" Width="100%" Font-Size="10px">
                                       
                    <%-- second (1) tab................................................................ --%>
                    <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="General User Info" >
                        <ContentTemplate>
                        <div style="font-size: 11px;">
                        
                            <div class="checkboxList" style="width: 450px; overflow: auto;">
                                <asp:CheckBoxList ID="chkUserRoles" runat="server" 
                                    RepeatDirection="Horizontal" />
                            </div>
                            
                            <br />
                            
                            <table>
                                <tr>
                                    <td valign="top" style="width: 445px">
                                        <asp:DetailsView AutoGenerateRows="False" DataSourceID="MemberData" ID="UserInfo"
                                            runat="server" OnItemUpdating="UserInfo_ItemUpdating" DefaultMode="Edit" HeaderText="General User Info">
                                            <Fields>
                                                <asp:BoundField DataField="UserName" HeaderText="User Name" ReadOnly="True"></asp:BoundField>
                                                <asp:BoundField DataField="Email" HeaderText="Email"></asp:BoundField>
                                                <asp:BoundField DataField="Comment" HeaderText="Comment"></asp:BoundField>
                                                <asp:CheckBoxField DataField="IsApproved" HeaderText="Active User"></asp:CheckBoxField>
                                                <asp:CheckBoxField DataField="IsLockedOut" HeaderText="Is Locked Out" 
                                                    ReadOnly="True">
                                                </asp:CheckBoxField>
                                                <asp:CheckBoxField DataField="IsOnline" HeaderText="Is Online" ReadOnly="True"></asp:CheckBoxField>
                                                <asp:BoundField DataField="CreationDate" HeaderText="CreationDate" ReadOnly="True">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LastActivityDate" HeaderText="LastActivityDate" ReadOnly="True">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LastLoginDate" HeaderText="LastLoginDate" ReadOnly="True">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LastLockoutDate" HeaderText="LastLockoutDate" ReadOnly="True">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LastPasswordChangedDate" HeaderText="LastPasswordChangedDate" ReadOnly="True"></asp:BoundField>
                                                <asp:TemplateField ShowHeader="False">
                                                    <EditItemTemplate>
                                                        <asp:Button ID="Button1" runat="server" CausesValidation="True" CommandName="Update" Text="Update User" />
                                                        <asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                                                        <asp:Button ID="Button4" runat="server" Text="Unlock User" OnClick="UnlockUser" OnClientClick="return confirm('Click OK to unlock this user.')" />
                                                        <asp:Button ID="Button5" runat="server" Text="Delete User" OnClick="DeleteUser" OnClientClick="return confirm('Are you sure? This will delete all information related to this user including the user profile.')" />
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Button ID="Button1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit User Info" />
                                                    </ItemTemplate>
                                                    <ControlStyle Font-Size="11px" />
                                                </asp:TemplateField>
                                            </Fields>
                                        </asp:DetailsView>
                                        
                                        <div>
                                            <asp:Literal ID="UserUpdateMessage" runat="server"></asp:Literal>
                                        </div>
                                        
                                        <br />
                                        
                                        <asp:ObjectDataSource ID="MemberData" runat="server" DataObjectTypeName="System.Web.Security.MembershipUser" SelectMethod="GetUser" UpdateMethod="UpdateUser" TypeName="System.Web.Security.Membership">
                                            <SelectParameters>
                                                <asp:QueryStringParameter Name="username" QueryStringField="username" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                        
                                    </td>
                                </tr>
                            </table>
                        
                        </div>    
                        </ContentTemplate>
                    </cc1:TabPanel>
                    
                    <%-- fourth (2) tab................................................................. --%>
                    <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="Change Password">
                    <ContentTemplate>
                        <div style="font-size: 11px;">
                        
                            <p></p>
                        
                          <table cellpadding="3" border="0">
                            <tr>
                              <td>Current Password:</td>
                              <td><asp:Textbox id="OldPasswordTextbox" runat="server" TextMode="Password" /></td>
                              <td><asp:RequiredFieldValidator id="OldPasswordRequiredValidator" runat="server" 
                                      ControlToValidate="OldPasswordTextbox" ErrorMessage="Required" 
                                      ValidationGroup="changepassword" Display="Dynamic" /></td>
                            </tr>
                            <tr>
                              <td>New Password:</td>
                              <td><asp:Textbox id="PasswordTextbox" runat="server" TextMode="Password" /></td>
                              <td><asp:RequiredFieldValidator id="PasswordRequiredValidator" runat="server" 
                                      ControlToValidate="PasswordTextbox" ErrorMessage="Required" 
                                      ValidationGroup="changepassword" Display="Dynamic" /></td>
                            </tr>
                            <tr>
                              <td>Confirm Password:</td>
                              <td><asp:Textbox id="PasswordConfirmTextbox" runat="server" TextMode="Password" /></td>
                              <td><asp:RequiredFieldValidator id="PasswordConfirmRequiredValidator" 
                                      runat="server" ControlToValidate="PasswordConfirmTextbox" 
                                      ErrorMessage="Required" ValidationGroup="changepassword" Display="Dynamic" />
                                  <asp:CompareValidator id="PasswordConfirmCompareValidator" runat="server" 
                                      ControlToValidate="PasswordConfirmTextbox" ControlToCompare="PasswordTextBox" 
                                      ErrorMessage="Confirm password must match password." 
                                      ValidationGroup="changepassword" Display="Dynamic" />
                              </td>
                            </tr>
                            <tr>
                              <td></td>
                              <td><asp:Button id="ChangePasswordButton" Text="Change Password" OnClick="ChangePassword_OnClick" runat="server" ValidationGroup="changepassword" /></td>
                            </tr>
                              <tr>
                                  <td>Current Password:</td>
                                  <td><asp:Label ID="lblCurrentPassword" runat="server" EnableViewState="False"></asp:Label></td>
                              </tr>
                          </table>
                          
                          <p></p>
                          
                          <asp:Label id="Msg" ForeColor="Maroon" runat="server" />
                        
                        </div>    
                        </ContentTemplate>
                    </cc1:TabPanel>
                    </cc1:TabContainer>
                
                <br />
                
                <%-- ajax update panel end --%>
            </ContentTemplate>
        </asp:UpdatePanel>
        
    </div>
    </form>
</body>
</html>
