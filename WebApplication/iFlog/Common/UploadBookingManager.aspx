<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UploadBookingManager.aspx.vb" Inherits="BetterclassifiedsWeb.UploadBookingManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Upload Booking Manager</title>
    <script language="javascript" type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow)
                oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog       
            else if (window.frameElement.radWindow)
                oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
            return oWindow;
        }

        function CloseRad() {
            GetRadWindow().Close();
        }  
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="uploadManager">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        
        <div style="margin-left:10px; margin-top:5px;">
            <telerik:RadProgressManager ID="RadProgressManager2" runat="server"
                ProgressIndicators="FilesCountBar,FilesCountPercent,TimeElapsed,TimeEstimated,CurrentFileName" />
            <telerik:RadProgressArea ID="RadProgressArea2" runat="server" />
        </div>
        
        <asp:Literal ID="lblUserMessage" runat="server" />

        <asp:Panel ID="pnlUserMessage" runat="server" CssClass="div-warning">
            To have a picture for your print ad, you will have to upgrade to our Premium Offer. Premium will also provide you with a Bold Header for your print ad.
        </asp:Panel>

        <asp:Panel ID="pnlOnline" runat="server">
            <h1><asp:Label ID="lblOnlineTitle" runat="server" Text="Online Ad Images" /></h1>
            <div class="breakSmall"></div>
            <paramountIt:MultipleFileUpload ID="paramountFileUpload" runat="server" DocumentCategory="OnlineAd" IsUploadOnSelect="true" />
            <div class="breakLarge"></div>
        </asp:Panel>
        
        <asp:Panel ID="pnlPrint" runat="server">
            
            <h1><asp:Label ID="lblPrintTitle" runat="server" Text="Print Image" /></h1>
            <div class="breakSmall"></div>
            <p style="margin-left: 12px; margin-bottom: 10px;">
                <asp:Label ID="lblPrice" runat="server" Font-Bold="true" Font-Italic="true" /></p>
            <paramountIt:WebImageMaker ID="paramountWebImageMaker" runat="server" DocumentCategory="LineAd"
                CancelButtonText="Cancel" ConfirmButtonText="Done" UploadButtonText="Upload" ImageUrl=""
                WorkingDirectory="C:\Paramount\WebImageMakerCache\" Format="Jpg" Quality="High" />
            <div class="breakLarge"></div>
        </asp:Panel>

        
        <div style="display: block; float: right; margin: 0px; margin-left: 510px;">
            <div class="save_close">
                <asp:LinkButton ID="linkCloseWindow" runat="server" Text="Save and Close" OnClientClick="javascript:CloseRad();" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
