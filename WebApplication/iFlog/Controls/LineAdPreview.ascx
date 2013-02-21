<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="LineAdPreview.ascx.vb" Inherits="BetterclassifiedsWeb.LineAdPreview" %>
<div class="line_ad_container">
	<div class="line_ad_header" runat="server" id="divHeader"><asp:Label ID="lblHeading" runat="server" Text=""></asp:Label></div>
    <div class="line_ad_image" runat="server" id="divImage"><asp:Image ID="imgLineAd" runat="server" /></div>
    <div class="line_ad_main"><asp:Label ID="lblAdText" runat="server" Text=""></asp:Label></div>
</div>