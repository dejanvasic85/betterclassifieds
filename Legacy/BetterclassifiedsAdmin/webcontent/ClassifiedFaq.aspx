<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master"
    CodeBehind="ClassifiedFaq.aspx.vb" Inherits="BetterclassifiedAdmin.ClassifiedFaq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyTitle" runat="server">
    Classified FAQ Page Content
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphUserNavigation" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContentBody" runat="server">
   
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" 
        BackgroundPosition="Center">
    </telerik:RadAjaxLoadingPanel>
    
    <paramountItCommon:GenericMessagePanel ID="msgPanel" runat="server" />
    
    <table>
        <tr>
            <td>Last Modified User:</td>
            <td><asp:Label ID="lblUser" runat="server" /></td>
        </tr>
        <tr>
            <td>Last Modified Date:</td>
            <td><asp:Label ID="lblDate" runat="server" /></td>
        </tr>
        <tr>
            <td>FAQ Page Enabled:</td>
            <td><asp:Label ID="lblEnabled" runat="server" /></td>
        </tr>
    </table>
    
    <telerik:RadEditor ID="radEditor" runat="server" />
    
    <asp:Button ID="btnSave" runat="server" Text="Save" />
    
</asp:Content>
