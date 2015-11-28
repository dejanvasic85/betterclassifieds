<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="LoginControl.ascx.vb" Inherits="BetterclassifiedAdmin.LoginControl" %>
<asp:login id="Login1" runat="server" cssclass="loginControl">
        <layouttemplate>
            <table runat="server" id="tblLayout" border="0" cellpadding="1" cellspacing="0" 
                style="border-collapse:collapse; width:300px;">
                <tr>
                    <td>
                        <table border="0" cellpadding="5">
                            
                            <tr>
                                <td align="right">
                                    <asp:label id="UserNameLabel" runat="server" associatedcontrolid="UserName">User Name:</asp:label>
                                </td>
                                <td>
                                    <asp:textbox id="UserName" runat="server" Width="200px"></asp:textbox>
                                    <asp:requiredfieldvalidator id="UserNameRequired" runat="server" 
                                        controltovalidate="UserName" errormessage="User Name is required." 
                                        tooltip="User Name is required." validationgroup="Login1">*</asp:requiredfieldvalidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:label id="PasswordLabel" runat="server" associatedcontrolid="Password">Password:</asp:label>
                                </td>
                                <td>
                                    <asp:textbox id="Password" runat="server" textmode="Password" Width="200px"></asp:textbox>
                                    <asp:requiredfieldvalidator id="PasswordRequired" runat="server" 
                                        controltovalidate="Password" errormessage="Password is required." 
                                        tooltip="Password is required." validationgroup="Login1">*</asp:requiredfieldvalidator>
                                </td>
                            </tr>
                           
                            <tr>
                                <td align="center" colspan="2" style="color:Red;">
                                    <asp:literal id="FailureText" runat="server" enableviewstate="False"></asp:literal>
                                </td>
                            </tr>
                            <tr>
                                <td  >
                                    <div style="margin-left:20px">
                                    
                                    
                                        
                                        </div>
                                </td>
                                <td>
                                    <asp:button id="LoginButton" runat="server" commandname="Login" text="Log In" 
                                        validationgroup="Login1" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </layouttemplate>
    </asp:login>