<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="MemberHeading.ascx.vb" Inherits="BetterclassifiedsWeb.MemberHeading" %>

<div id="mainHeaderMyAccount">
    <asp:Image ID="imgAccHeader" runat="server" ImageUrl="~/Resources/Images/my_account_header.gif" AlternateText="My Account" />
    <h2>
        Welcome, 
        <asp:LoginName ID="LoginName1" runat="server"  />
    </h2>
        <h3>
            <asp:Label ID="lblHeader" runat="server" Text=""></asp:Label></h3>

</div>