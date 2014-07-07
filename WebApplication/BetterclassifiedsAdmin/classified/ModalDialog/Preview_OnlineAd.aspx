<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Preview_OnlineAd.aspx.vb" Inherits="BetterclassifiedAdmin.Preview_OnlineAd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 15px;">
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" /><br /><br />
    
        <asp:DetailsView ID="dtlOnlineAd" runat="server" AutoGenerateRows="false"   
                         DefaultMode="ReadOnly" HeaderText="Online Ad Details" CellPadding="10">
            <Fields>
                <asp:BoundField HeaderText="ID" DataField="OnlineAdId" />
                <asp:BoundField HeaderText="Views" DataField="NumOfViews" />
                <asp:BoundField HeaderText="Heading" DataField="Heading" />
                <asp:BoundField HeaderText="Description" DataField="Description" />
                <asp:BoundField HeaderText="Price" DataField="Price" DataFormatString="{0:C}" />
                <asp:BoundField HeaderText="Location" DataField="Location" />
                <asp:BoundField HeaderText="Area" DataField="LocationArea" />
                <asp:BoundField HeaderText="Name" DataField="ContactName" />
                <asp:BoundField HeaderText="Phone" DataField="ContactPhone" />
                <asp:BoundField HeaderText="Email" DataField="ContactEmail" />
            </Fields>
        </asp:DetailsView>
        
        <div style="margin-top: 5px;">
            <asp:Table ID="tblImages" runat="server" CellPadding="10">
                <asp:TableRow>
                    <asp:TableHeaderCell>
                        <asp:Label ID="Label2" runat="server" Text="Images"></asp:Label>
                    </asp:TableHeaderCell>
                </asp:TableRow>
                <asp:TableRow ID="rowImages" runat="server"></asp:TableRow>
            </asp:Table>
        </div>
        
        <div style="margin-top: 5px;">
            <table>
                <tr>
                    <td>Status:</td>
                    <td><asp:DropDownList ID="ddlStatus" runat="server">
                            <asp:ListItem Text="Pending" Value="1" />
                            <asp:ListItem Text="Approved" Value="2" />
                            <asp:ListItem Text="Cancelled" Value="3" />
                        </asp:DropDownList></td>
                    <td><asp:Button ID="btnUpdateStatus" runat="server" Text="Update Status" /></td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
