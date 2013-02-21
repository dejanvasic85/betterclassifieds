<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="MasterHeader.ascx.vb" Inherits="BetterclassifiedAdmin.MasterHeader" %>
<%@ Register src="MasterUserStats.ascx" tagname="adminUserStats" tagprefix="uc1" %>

<%-- div wrapper for header control --%>
<div class="headerControl">

<%-- header bg image 
<div class="headerTitleImage"></div>--%>

<%-- header title text --%>
    <div class="headerTitleText">
        <asp:HyperLink ID="lnkTitle" runat="server" NavigateUrl="~/Default.aspx"
            Text="Betterclassified Web Site Administration Tool " />
    </div>

<div class="loginNameAndStatus">

    Welcome!
    <asp:LoginName ID="LoginName1" runat="server" />
    &nbsp;
    <asp:LoginStatus ID="LoginStatus1" runat="server" />
    
    <%-- number of registered and online users
    <uc1:adminUserStats ID="adminUserStats1" runat="server" /> --%>
<%--
    <p>This website is best viewed with Internet Explorer 7</p>--%>
</div> 

</div>