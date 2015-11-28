<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Default.Master" CodeBehind="GetPassword.aspx.vb" Inherits="BetterclassifiedsWeb.GetPassword" %>

<asp:Content ID="mainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <div>
    <h5>Please enter your email address</h5>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <asp:TextBox runat="server" ID="emailBox">  
    </asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
        ErrorMessage="please enter your email address" ControlToValidate="emailBox" 
            SetFocusOnError="True">&nbsp;</asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ErrorMessage="Please enter a valid email address" ControlToValidate="emailBox" 
            SetFocusOnError="True" 
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&nbsp;</asp:RegularExpressionValidator>
    <asp:Label Visible="true" ID="errorLabel" runat="server"></asp:Label>
    </div>
    <div>
        <asp:Button ID="SubmitEmail" runat="server" Text="Submit" />
    </div>
</asp:Content>