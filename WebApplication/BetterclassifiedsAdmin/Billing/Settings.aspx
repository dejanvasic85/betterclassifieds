<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Settings.aspx.vb" Inherits="BetterclassifiedAdmin.Settings"
    MasterPageFile="~/masterpage/MasterPage.master" %>

<asp:Content ID="content1" ContentPlaceHolderID="cphContentHeading" runat="server">
Billing Settings
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContentBody" runat="server">
    <div>
        <paramountIt:Settings ID="settings" runat="server" />
    </div>
</asp:Content>
