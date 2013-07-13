<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MasterWithRightBar.Master"
    CodeBehind="Publications.aspx.vb" Inherits="BetterclassifiedsWeb.Publications"
    Title="Publications, Editions and Deadlines" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    
    <div id="mainInfo">
        <div id="mainHeaderInfo">
            <h2>
                Publications and Deadlines</h2>
        </div>
        <div id="mainContentInfo">
            <asp:DataList ID="listPublications" runat="server" Width="570px" CellPadding="5" CellSpacing="0">
                <ItemTemplate>
                    <asp:Image ID="imgPublication" runat="server" />
                    <h4>
                        About <asp:Label ID="lblPublication" runat="server" Text='<%# Eval("Title") %>'></asp:Label></h4>
                    <p>
                        <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' /></p>
                    <h4>
                        Next Deadline</h4>
                    <p>
                        <asp:Label ID="Label2" runat="server" Text='<%# String.Format("{0}{1:f}", "Deadline: ", Eval("NextDeadline")) %>'></asp:Label></p>
                    <h4>
                        Next Publication Date</h4>
                    <p>
                        <asp:Label ID="Label1" runat="server" Text='<%# String.Format("{0}{1:D}", "Next Edition: ", Eval("NextEdition")) %>'></asp:Label>
                    </p>
                    <spacer>&nbsp;</spacer>
                </ItemTemplate>
            </asp:DataList>
        </div>
        <div id="mainFooterInfo">
            <em>RETURN TO:</em> <a href="#0">TOP</a> |
            <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="~/Default.aspx" Text="HOME" /></div>
    </div>
</asp:Content>
