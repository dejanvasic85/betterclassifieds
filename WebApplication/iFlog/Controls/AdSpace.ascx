<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="AdSpace.ascx.vb" Inherits="BetterclassifiedsWeb.AdSpace" %>

<asp:Repeater ID="rptAds" runat="server">
    <ItemTemplate>
        <asp:HyperLink ID="lnkAd" runat="server" 
            NavigateUrl='<%# String.Format("{0}", Eval("AdLinkUrl")) %>'
            Target='<%# Eval("AdTarget") %>'
            ToolTip='<%# Eval("ToolTipText") %>' />
    </ItemTemplate>
</asp:Repeater>