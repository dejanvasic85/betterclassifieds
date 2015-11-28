<%@ Page Title="Login to Classies" Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="BetterclassifiedsWeb.Login1" MasterPageFile="~/Master/Default.Master" %>
<%@ Register Src="~/Controls/Member/Login.ascx" tagname="Login" tagprefix="ucx" %>


<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="content1">
   
   <div class="clearFloat">
   
       <div id="contentBodyAccounts">
            <div id="mainLoginRegister">
                <div id="mainHeaderMyAccount">
                    <h2>Login to Classies with your username and password</h2>
                </div>
                <div id="bookAdMainContent">
                    &nbsp;
                </div>
                <div id="mainContentMyAccount">
                    
                    <ucx:Login ID="ucxLogin" runat="server" />
                              
                    <div id="bookAdMainContent">
                        &nbsp;
                    </div>
                    <div id="myAccountTableChgDet">
                        <table width="570" border="0" align="center" cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td width="190">
                                </td>
                                <td width="250">
                                    <h6 align="right">
                                        Don't have username or password?</h6>
                                </td>
                                <td width="130">
                                </td>
                            </tr>
                            <tr>
                                <td width="190">
                                </td>
                                <td width="250">
                                    <h6 align="right">
                                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Register.aspx">Click here to register.</asp:HyperLink></h6>
                                </td>
                                <td width="130">
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="bookAdMainContent">
                        &nbsp;
                    </div>
                </div>
            </div>
            <br />
       </div>
       
    </div>   
</asp:Content>