<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Preview.aspx.vb" Inherits="BetterclassifiedsWeb.Preview" %>

<%@ Register Src="~/Controls/OnlineAdViewWithDiv.ascx" TagName="OnlineAd" TagPrefix="ucx" %>
<%@ Register src="~/Controls/TutorAdView.ascx" tagName="TutorView" tagPrefix="ucx" %>

<!DOCTYPE>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
        
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <div style="padding:10px" id="divAdViewControls" runat="server">
        <ucx:OnlineAd ID="ucxOnlineAd" runat="server" PreviewOnly="true" />
        <ucx:TutorView ID="ucxTutors" runat="server" Visible="False"/>
    </div>
    </form>
</body>
</html>
