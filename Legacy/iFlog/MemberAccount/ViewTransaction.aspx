<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ViewTransaction.aspx.vb" Inherits="BetterclassifiedsWeb.ViewTransaction" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
        .tranContent
        {
            padding:20px;
        }
        .items
        {
            width: 75%;
            text-align: left;
            float: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="tranContent">
        <p>Dharma Media Pty Ltd
            <br />ABN 54 117 132 402
            <br/>Locked Bag 2001, Clifton Hill VIC 3068
        </p>
        
        
        <p>Tel 03 9421 4499<br />
        Email <a href="mailto:accounts@streetpress.com.au">accounts@streetpress.com.au</a></p>

        <div style="mso-element:para-border-div;border:none;border-bottom:solid windowtext 1.5pt;padding:0cm 0cm 1.0pt 0cm"></div>    
        
        <br />
        
        <asp:DetailsView ID="dtlUser" runat="server" AutoGenerateRows="false" Width="400px" EnableViewState="false"
            DataSourceID="srcUserProfile" EmptyDataText="cannot find user" CellPadding="3" GridLines="None">
            <FieldHeaderStyle Width="200px" />
            <Fields>
                <asp:BoundField HeaderText="To:" DataField="FullName" ItemStyle-Font-Bold="true" />
                <asp:BoundField HeaderText="Business Name:" DataField="BusinessName" />
                <asp:BoundField HeaderText="Address:" DataField="Address1" />
                <asp:BoundField HeaderText="Suburb:" DataField="City" />
                <asp:BoundField HeaderText="State:" DataField="State" />
                <asp:BoundField HeaderText="Post Code:" DataField="PostCode" />
            </Fields>
        </asp:DetailsView>
        <asp:ObjectDataSource ID="srcUserProfile" runat="server" SelectMethod="GetUserProfile" EnableViewState="false" TypeName="BetterclassifiedsCore.CRM.CrmController">
        </asp:ObjectDataSource>
        
        <h4>TAX INVOICE</h4>
        
        <asp:DetailsView ID="dtlTransaction" runat="server" AutoGenerateRows="false" Width="400px"  EnableViewState="false"
            DataSourceID="srcTransaction" EmptyDataText="Unable to retrieve transaction information" CellPadding="3" GridLines="None">
            <FieldHeaderStyle Width="200px" />
            <Fields>
                <asp:BoundField HeaderText="Username:" DataField="UserId" ItemStyle-Font-Bold="true" />
                <asp:BoundField HeaderText="Invoice Date:" DataField="TransactionDate" DataFormatString="{0:dd-MMM-yyyy}" />
                <asp:BoundField HeaderText="Invoice Number:" DataField="Title" />
                <asp:BoundField HeaderText="Description:" DataField="Description" />
                <asp:TemplateField HeaderText="Paid Using:">
                    <ItemTemplate>
                        <asp:Label ID="lblType" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Sub Total" DataField="AmountBeforeGST" DataFormatString="{0:C}" />
                <asp:BoundField HeaderText="GST" DataField="GSTAmount" DataFormatString="{0:C}" />
                <asp:BoundField HeaderText="Total:" DataField="Amount" DataFormatString="{0:C}" ItemStyle-Font-Bold="true" HeaderStyle-Font-Bold="true" />
            </Fields>
        </asp:DetailsView>
        <asp:ObjectDataSource ID="srcTransaction" runat="server" EnableViewState="false" SelectMethod="GetTransaction" TypeName="BetterclassifiedsCore.BookingController" />
        
        <hr class="items" />
        <h5>Invoice Items</h5>
        
        <asp:GridView ID="grdInvoiceItems" runat="server" CellPadding="4"  EnableViewState="false"
            AutoGenerateColumns="false" CssClass="items" GridLines="None" EmptyDataText="Unable to find any Invoice Items"
            DataSourceID="srcItems">
            <RowStyle HorizontalAlign="Center" />
            <HeaderStyle HorizontalAlign="Center" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" />
            <Columns>
                <asp:BoundField HeaderText="Ad ID" DataField="AdDesignId" />
                <asp:TemplateField HeaderText="Ad Type">
                    <ItemTemplate>
                        <asp:Literal ID="lblAdType" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Photos" DataField="Photos" />
                <asp:BoundField HeaderText="Category" DataField="Category" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="srcItems" runat="server"  EnableViewState="false" SelectMethod="GetTransactionItems" TypeName="BetterclassifiedsCore.BookingController" />
        
        <br /><br />
        <div style="mso-element:para-border-div;border:none;border-bottom:solid windowtext 1.5pt;padding:0cm 0cm 1.0pt 0cm"></div>    
        <br />
        
        <p>Thank You</p>
        
    </div>
    </form>
</body>
</html>
