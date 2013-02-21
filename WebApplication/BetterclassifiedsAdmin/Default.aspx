<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="Default.aspx.vb" Inherits="BetterclassifiedAdmin._Default" theme="blue" %>
<asp:content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:content>
<asp:content ID="Content2" ContentPlaceHolderID="cphBodyTitle" runat="server">
    Home
</asp:content>
<asp:content ID="Content3" ContentPlaceHolderID="cphUserNavigation" runat="server">
</asp:content>
<asp:content ID="Content4" ContentPlaceHolderID="cphContentBody" runat="server">
    <div id="LoginAsPanel" style="clear:both">
        <div style=" float:left; width:60px;">
            <asp:image id="Image1" runat="server" imageurl="~/images/UserIcon_User.png" />
        </div>
        <div style="float:left">
            <asp:Label ID="Label1" runat="server" Text="Welcome" Font-Bold="true" />
            <asp:loginname id="LoginName1" runat="server" />
            <asp:loginstatus id="LoginStatus1" runat="server" />
        </div>
        <div style="clear:both"> </div>
    </div> 
</asp:content>
