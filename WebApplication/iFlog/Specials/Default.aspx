<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Default.Master" CodeBehind="Default.aspx.vb" Inherits="BetterclassifiedsWeb._Default6"
        Title="Betterclassifieds Specials" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div id="bookAdMainContent">
        <p>
            <asp:Label ID="sessionLabel" runat="server" Text="Your session has expired. You will need to start with special rate selection." ForeColor="Red" />
        </p>
    </div>
    
    <h3>
        <asp:Label ID="Label1" runat="server" Text="Check out our Special Offers!"></asp:Label></h3>
        
    <asp:Label ID="Label5" runat="server" Text="Search all our special offers catalog by Category." />
    <asp:UpdatePanel ID="pnlUpdate1" runat="server">
    <ContentTemplate>
    <table cellpadding="3">
        <tr>
            <td align="right">
                <asp:Label ID="Label6" runat="server" Text="Category" />:</td>
            <td>
                <asp:DropDownList ID="ddlMainCategory" runat="server" 
                    DataTextField="Title" DataValueField="MainCategoryId" AutoPostBack="True" />
            </td>
            <td align="right">
                <asp:Label ID="Label7" runat="server" Text="Sub Category" />:</td>
            <td>
                <asp:DropDownList ID="ddlSubCategory" runat="server" 
                    DataTextField="Title" DataValueField="MainCategoryId" />
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="Search" /></td>
        </tr>
    </table>
    
    <asp:DataList ID="listSpecials" runat="server" Width="100%" BackColor="White" 
            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
            GridLines="Both">
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <ItemStyle ForeColor="#000066" />
        <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <HeaderTemplate>
            <h4>
                <asp:Label ID="Label2" runat="server" Text="Specials Currently Available"></asp:Label></h4>
        </HeaderTemplate>
        
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        
        <ItemTemplate>
            
            <table width="100%" cellpadding="5">
                <tr style="background-color: Yellow;">
                    <th colspan="2">
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("Title") %>'></asp:Label></th>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("Description") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
                        <asp:HyperLink ID="HyperLink1" runat="server" Text="Start Booking" NavigateUrl='<%# String.Format("{0}{1}{2}{3}", "~/Booking/BookSpecial.aspx?specialId=", Eval("SpecialRateId"), "&mainCategoryId=", Eval("MainCategoryId")) %>' /></td>
                </tr>
            </table>
        </ItemTemplate>
        
    </asp:DataList>
    </ContentTemplate>
    </asp:UpdatePanel>
        
</asp:Content>
