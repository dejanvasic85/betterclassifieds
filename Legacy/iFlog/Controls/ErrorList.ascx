<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ErrorList.ascx.vb" Inherits="BetterclassifiedsWeb.ErrorList" %>

<div id="divError" runat="server" style="color: Black; padding: 10px;" visible="false">
    
    <div style="font-size: 12px; border: 2px solid red; padding: 5px; width: 400px; background-color: Pink;">
        <asp:Label ID="Label5" runat="server" Text="Please review the following:" Font-Bold="true" />
   
        <asp:BulletedList ID="bulletErrorList" runat="server" />
    </div>
</div>