<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Payment.ascx.vb" Inherits="BetterclassifiedsWeb.Controls.Booking.Payment" %>

<fieldset>
<legend>
    <asp:Label ID="lblPaymentDetails" runat="server" Text="Payment Details"></asp:Label></legend>
<table cellpadding="5" cellspacing="2">
    <tr>
        <td>
            <asp:Label ID="lblAmountLbl" runat="server" Text="Amount to be charged" />:</td>
        <td>
            <asp:Label ID="lblAmount" runat="server" Text="" /></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblCreditType" runat="server" Text="Credit Card Type"></asp:Label>:</td>
        <td>
            <asp:DropDownList ID="ddlCreditCardType" runat="server">
                <asp:ListItem>Visa</asp:ListItem>
                <asp:ListItem>Master Card</asp:ListItem>
                <asp:ListItem>AMEX</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblCreditCardLbl" runat="server" Text="Credit Card Number"></asp:Label>:</td>
        <td>
            <asp:TextBox ID="txtCreditCardNumber" runat="server" Width="160px"></asp:TextBox></td>
        <td>
            <asp:RequiredFieldValidator ID="valRequiredCC" runat="server" ErrorMessage="Credit Card Required." Text="*"
                 ControlToValidate="txtCreditCardNumber" EnableClientScript="false"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblCardExpiryLbl" runat="server" Text="Card Expiry"></asp:Label>:</td>
        <td>
            <asp:DropDownList ID="ddlMonthExpiry" runat="server">
                <asp:ListItem>01</asp:ListItem>
                <asp:ListItem>02</asp:ListItem>
                <asp:ListItem>03</asp:ListItem>
                <asp:ListItem>04</asp:ListItem>
                <asp:ListItem>05</asp:ListItem>
                <asp:ListItem>06</asp:ListItem>
                <asp:ListItem>07</asp:ListItem>
                <asp:ListItem>08</asp:ListItem>
                <asp:ListItem>09</asp:ListItem>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>11</asp:ListItem>
                <asp:ListItem>12</asp:ListItem>
            </asp:DropDownList>
            &nbsp;
            <asp:DropDownList ID="ddlYearExpiry" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblNameOnCardLbl" runat="server" Text="Name on Card"></asp:Label>:</td>
        <td>
            <asp:TextBox ID="txtNameOnCard" runat="server" Width="160px"></asp:TextBox></td>
        <td>
            <asp:RequiredFieldValidator ID="val" runat="server" ErrorMessage="Name on Card is Required." Text="*"
                ControlToValidate="txtNameOnCard" EnableClientScript="false"></asp:RequiredFieldValidator></td>
    </tr>
</table>
</fieldset>