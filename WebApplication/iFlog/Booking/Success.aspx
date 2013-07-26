<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Success.aspx.vb" Inherits="BetterclassifiedsWeb.Success" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Please wait</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style=" text-align:center; margin:25%">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/Images/ajax-loading.gif" AlternateText="Please wait" />
        <p>Please wait</p>
      </div>
    </form>
</body>
</html>
