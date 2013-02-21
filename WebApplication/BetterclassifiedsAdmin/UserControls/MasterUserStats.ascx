<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="MasterUserStats.ascx.vb" Inherits="BetterclassifiedAdmin.MasterUserStats" %>
<div class="whosOnlineWrap">

<a href="Users.aspx" title="The total number of registered users">Registered Users: <asp:Literal runat="server" ID="lblTotalUsers" /></a>

&nbsp;

<a href="Online_users.aspx" title="The number of users currently visiting the site">Online: <asp:Literal runat="server" ID="lblOnlineUsers" /></a>

</div>