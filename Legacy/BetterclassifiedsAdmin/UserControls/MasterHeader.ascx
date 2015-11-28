<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="MasterHeader.ascx.vb" Inherits="BetterclassifiedAdmin.MasterHeader" %>

<%-- div wrapper for header control --%>
<div class="headerControl">
    
    <%-- header title text --%>
    <div class="headerTitleText">
        <asp:HyperLink ID="lnkTitle" runat="server" NavigateUrl="~/Default.aspx"
            Text="Classies Administration " />
    </div>

    <div class="loginNameAndStatus">
        Welcome!
    <asp:LoginName ID="LoginName1" runat="server" />
        &nbsp;
    <asp:LoginStatus ID="LoginStatus1" runat="server" />
    </div>

</div>
