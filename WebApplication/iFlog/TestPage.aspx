<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TestPage.aspx.vb" Inherits="BetterclassifiedsWeb.TestPage"
    Theme="Plain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <style type="text/css">
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 10px; margin-top: 20px;">
        <paramountIt:LineAdTextBox ID="lineAdTextBox" runat="server" MaximumWords="10" CssClass="lineAdTextbox"/>
        <asp:Button ID="btnTest" runat="server" Text="Test" />
    </div>
    </form>
</body>
</html>
