<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Entry.aspx.vb" Inherits="BetterclassifiedAdmin.Entry" theme="blue" stylesheettheme="blue"%>

<%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    
</head>
<body style="background-color:#9AB8D7; margin-top:20%">
    <form id="form1" runat="server">
    <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
    <div style="text-align:center">
       
        <div id="login-panel">
            <div class="header">
                <div class="paneltext">
                    Please enter your username and password to login..
                </div>
            </div>
            <div class="body">
                <asp:login id="Login1" runat="server" width="350px" Height="150px" titletext="">
                </asp:login>
            </div>
        </div>
         
    </div>
    </form>
</body>
</html>
