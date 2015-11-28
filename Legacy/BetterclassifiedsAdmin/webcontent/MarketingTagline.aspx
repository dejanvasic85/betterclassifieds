<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="MarketingTagline.aspx.vb" Inherits="BetterclassifiedAdmin.MarketingTagline" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyTitle" runat="server">
    Margeting Tagline
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphUserNavigation" runat="server">
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContentHeading" runat="server">
    Marketing Tagline
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphContentBody" runat="server">
    
    <paramountItCommon:GenericMessagePanel ID="msgPanel" runat="server" />

    <p>The following content will be displayed during the booking process within the design ad stage.</p>

    <table>
        <tr>
            <td>Last Modified User:</td>
            <td><asp:Label ID="lblUser" runat="server" /></td>
        </tr>
        <tr>
            <td>Last Modified Date:</td>
            <td><asp:Label ID="lblDate" runat="server" /></td>
        </tr>
    </table>

    <telerik:RadEditor ID="radEditor" runat="server" />

    <asp:Button ID="btnSave" runat="server" Text="Save" />
</asp:Content>
