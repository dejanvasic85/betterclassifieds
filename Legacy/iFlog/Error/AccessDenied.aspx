<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Default.Master" CodeBehind="AccessDenied.aspx.vb" Inherits="BetterclassifiedsWeb.AccessDenied" 
    title="Access denied to requested content." %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Access Denied</h3>
    <p>Access to the requested content is denied. Please 
    <asp:HyperLink ID="lnkContact" runat="server" Text="contact" />
    TheMusic for any further issues. </p>
</asp:Content>
