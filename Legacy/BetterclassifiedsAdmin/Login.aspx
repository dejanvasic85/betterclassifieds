<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master"
    CodeBehind="Login.aspx.vb" Inherits="BetterclassifiedAdmin.Login" Theme="blue"%>

<%@ Register Src="controls/LoginControl.ascx" TagName="LoginControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBodyTitle" runat="server">
    Login
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphUserNavigation" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContentBody" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Welcome to Betterclassifieds Administration" Font-Bold="true" />
    <p>This system is for authorised personnel only. If you need to obtain access, please contact your system administrator.</p>

    <div class="loginControlContainer">
        <uc1:LoginControl ID="LoginControl1" runat="server" Width="500px" />
    </div>
</asp:Content>
