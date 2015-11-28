<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="AdSpaceNavigation.ascx.vb" Inherits="BetterclassifiedAdmin.AdSpaceNavigation" %>
<asp:Repeater ID="rptAdSettingLinks" runat="server">
    <ItemTemplate>
        <div class="userCategories">
            <asp:HyperLink ID="lnkSpace" runat="server" 
                Text='<%# Eval("Title") %>'
                NavigateUrl='<%# String.Format("~/classified/AdSpaces.aspx?settingId={0}", Eval("SettingId")) %>' />
        </div>
    </ItemTemplate>
</asp:Repeater>