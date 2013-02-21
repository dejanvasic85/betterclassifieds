<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="FlogID.ascx.vb" Inherits="BetterclassifiedsWeb.FlogID" %>

<asp:Panel ID="pnlFlogIdSearch" runat="server" DefaultButton="btnSearch">
<div id="sidebarIflogId">
    <div id="sidebarIflogIdButton">
        <p>
            <asp:Button ID="btnSearch" runat="server" Text="iFlog ID" /></p>
    </div>
    <div id="sidebarIflogTextBox">
        <asp:TextBox ID="txtIFlogID" runat="server" Width="70px"></asp:TextBox>
    </div>
</div>
</asp:Panel>