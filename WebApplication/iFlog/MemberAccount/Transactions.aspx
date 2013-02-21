<%@ Page Title="iFlog User Transactions" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MemberDetails.master"
    CodeBehind="Transactions.aspx.vb" Inherits="BetterclassifiedsWeb.Transactions" %>

<%@ Register Src="~/MemberAccount/MemberHeading.ascx" TagName="MemberHeading" TagPrefix="ucx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="memberHeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="memberContentMain" runat="server">

    <ucx:MemberHeading ID="ucxHeading" runat="server" HeadingText="My Transactions" />
    
    <div id="mainContentMyAccount">
    
        <div id="bookAdMainContent">
            <p>You may view and print your transactions for the past <asp:Label ID="lblMonths" runat="server" /> month(s).</p>
        </div>    
        
        <br /><br />
        
        <div class="UserListPanel">
            
            <asp:GridView ID="grdTransactions" runat="server" Width="740" AutoGenerateColumns="false" 
                            EmptyDataText="You have no transaction history available." CellPadding="0"
                            GridLines="Horizontal" AllowSorting="false" EnableViewState="false">
                <HeaderStyle CssClass="myAccountTableItemHead" />
                <RowStyle Height="28" />
                
                <Columns>
                    <asp:BoundField HeaderText="Ref No" DataField="Ref_No" ItemStyle-CssClass="myAccountTableItemBody" />
                    <asp:BoundField HeaderText="Date" DataField="TransactionDate" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-CssClass="myAccountTableItemBody" />
                    <asp:BoundField HeaderText="Price" DataField="Amount" DataFormatString="{0:C}" ItemStyle-CssClass="myAccountTableItemBody" />
                    <asp:BoundField HeaderText="Category" DataField="Category" ItemStyle-CssClass="myAccountTableItemBody" />
                    <asp:TemplateField HeaderText="Payment" ItemStyle-CssClass="myAccountTableItemBody">
                        <ItemTemplate>
                            <asp:Literal ID="lblType" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:HyperLinkField HeaderText="Invoice" Text="View"
                        datanavigateurlformatstring="~/MemberAccount/ViewTransaction.aspx?ref={0}" datanavigateurlfields="Ref_No" 
                        target="_blank" ItemStyle-CssClass="myAccountTableItemBody"  />
                </Columns>
                
            </asp:GridView>
            
        </div>
    </div>

</asp:Content>
