<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="RecentOnlineAdList.ascx.vb" Inherits="BetterclassifiedsWeb.Controls.Search.RecentOnlineAdList" %>


<asp:DataList ID="listRecentlyListed" RepeatLayout="Table" RepeatColumns="3" runat="server"
    GridLines="None" RepeatDirection="Horizontal"
    CellPadding="0" CellSpacing="0" Width="100%">
    <ItemStyle VerticalAlign="Top" />
    <ItemTemplate>
        <div class="itemContainer">
            <div style="height: 200px;">
                <div id="categoryHeader">
                    <h3>
                        <asp:LinkButton ID="lnkCategory" runat="server" CssClass="categoryHeader" Text='<%# Eval("Category") %>' CommandArgument='<%# Eval("CategoryId") %>' OnCommand="CategoryClick" /></h3>
                </div>

                <div id="categoryBody">
                    <h4 class="word-wrap">
                        <asp:HyperLink ID="lnkHeadingLink" runat="server" Text='<%# Eval("Heading") %>' /></h4>
                    <p>
                        <asp:HyperLink ID="lnkImageLink" runat="server" />
                    </p>
                    <p class="word-wrap">
                        <asp:Literal ID="litText" runat="server" Text='<%# Eval("AdText") %>' />
                    </p>
                </div>
            </div>

            <div id="categoryMore">
                <p>
                    <asp:HyperLink ID="lnkDetailLink" runat="server" Text="More..."></asp:HyperLink>
                </p>
            </div>

        </div>
    </ItemTemplate>
</asp:DataList>