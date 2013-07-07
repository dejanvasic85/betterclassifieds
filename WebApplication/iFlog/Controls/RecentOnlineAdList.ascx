<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="RecentOnlineAdList.ascx.vb" Inherits="BetterclassifiedsWeb.Controls.Search.RecentOnlineAdList" %>


<asp:datalist ID="listRecentlyListed" RepeatLayout="Table" RepeatColumns="3" runat="server"
        GridLines="None" RepeatDirection="Horizontal"
        CellPadding="0" CellSpacing="0" Width="100%">
        
    <ItemStyle VerticalAlign="Top" />
    
    <ItemTemplate>
    
        
        <div class="itemContainer">
            <div style="height: 200px;">
                <div id="categoryHeader">
                    <h3> 
                        <asp:linkbutton ID="lnkCategory" runat="server" CssClass="categoryHeader" Text='<%# Eval("Category") %>' 
                            commandargument='<%# Eval("CategoryId") %>' oncommand="CategoryClick" /></h3>
                </div>
                
                <div id="categoryBody">
                    <h4 class="word-wrap">
                        <asp:HyperLink ID="HyperLink4" runat="server" Text='<%# Eval("Heading") %>' 
                            NavigateUrl='<%# String.Format("{0}{1}", "~/OnlineAds/AdView.aspx?preview=false&id=", Eval("OnlineAdId")) %>' /></h4>
                    <p>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# String.Format("{0}{1}", "~/OnlineAds/AdView.aspx?preview=false&id=", Eval("OnlineAdId")) %>'>
                            <asp:Image ID="imgDocument" runat="server" ImageUrl='<% Eval("DocumentId") %>' />
                        </asp:HyperLink></p>
                    <p class="word-wrap">
                        <asp:Literal ID="litText" runat="server" Text='<%# Eval("AdText") %>' /></p>
                </div>
            </div>
            
            <div id="categoryMore">
                <p>
                    <asp:HyperLink ID="HyperLink5" runat="server" Text="More..." NavigateUrl='<%# String.Format("{0}{1}", "~/OnlineAds/AdView.aspx?preview=false&id=", Eval("OnlineAdId")) %>'></asp:HyperLink></p>
            </div>
            
        </div>
    </ItemTemplate>
</asp:datalist>