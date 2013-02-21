<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DataTypeDetails.aspx.vb" Inherits="BetterclassifiedAdmin.DataTypeDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>What are the Data Types?</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 20px">
        <asp:Panel ID="pnlDataExplanations" runat="server">
        <p>
            <asp:Label ID="Label9" runat="server" Text="Data Types are the values you may enter for an application setting."></asp:Label><br />
            <asp:Label ID="Label10" runat="server" Text="Please read the following to understand:"></asp:Label></p>    
        <table width="100%" cellpadding="4">
            <tr valign="top">
                <th align="left"><asp:Label ID="Label1" runat="server" Text="int" Font-Bold="true" /></th>
                <td><asp:Label ID="Label2" runat="server" Text="A number value with <b>NO</b> decimal points."/></td>
            </tr>
            <tr valign="top">
                <th align="left"><asp:Label ID="Label3" runat="server" Text="string" Font-Bold="true" /></th>
                <td><asp:Label ID="Label4" runat="server" Text="More than one word or number allowed. Please avoid using apostrophies and other non alpha-numeric values unless you have Paramount IT approval." /></td>
            </tr>
            <tr valign="top">
                <th align="left"><asp:Label ID="Label5" runat="server" Text="bit" Font-Bold="true" /></th>
                <td><asp:Label ID="Label6" runat="server" Text="True or False value. Cannot be anything else." /></td>
            </tr>
            <tr valign="top">
                <th align="left"><asp:Label ID="Label7" runat="server" Text="decimal" Font-Bold="true" /></th>
                <td><asp:Label ID="Label8" runat="server" Text="A number that may contain decimal values. For e.g. Prices use decimals - 12.99" /></td>
            </tr>
        </table>        
    </asp:Panel> 
    </div>
    </form>
</body>
</html>
