<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UploadFiles.aspx.vb" Inherits="BetterclassifiedsWeb.UploadFiles" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        
        <paramountIt:MultipleFileUpload ID="fileUpload" runat="server" MaxFiles="3" DocumentCategory="OnlineAd" ReferenceData="Dejan Test" />
        
    </div>
    </form>
</body>
</html>
