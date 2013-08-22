<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="OnlineAdView.ascx.vb"
    Inherits="BetterclassifiedsWeb.OnlineAdView" %>

<div id="mainHeaderItemPage">
    <div id="divSitemap" runat="server">
        <h5>You are now in: 
            <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="~/Default.aspx" Text="Home" />
            > 
            <asp:LinkButton ID="lnkCategory" runat="server" />
            >
            <asp:LinkButton ID="lnkSubCategory" runat="server" />
            > 
            <asp:Label ID="lblID" runat="server" />
        </h5>
    </div>

    <div id="mainHeaderItemPageTitleCost">
        <table width="400" border="0" cellspacing="0px" cellpadding="0px">
            <tr>
                <td>
                    <h2>
                        <asp:Label ID="lblHeading" runat="server" Text="" /></h2>
                </td>
            </tr>
            <tr runat="server" id="objPrice">
                <td>
                    <h3>
                        <asp:Label ID="Label3" runat="server" Text="Price" Font-Bold="true" />:
                        <asp:Label ID="lblPrice" runat="server" Text=""></asp:Label></h3>
                </td>
            </tr>
        </table>


    </div>
    <div id="mainHeaderItemPageIDHits" runat="server" style="float: right; margin-right: 10px;">
        <table width="125" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <h6>ID:</h6>
                </td>
                <td>
                    <h6>
                        <asp:Label ID="lblIFlogID" runat="server" Text=""></asp:Label></h6>
                </td>
            </tr>
            <tr>
                <td>
                    <h6>Hits:</h6>
                </td>
                <td>
                    <h6>
                        <asp:Label ID="lblNumOfViews" runat="server" Text=""></asp:Label></h6>
                </td>
            </tr>
        </table>
    </div>
    <div style="clear: both"></div>
    <div id="bottomLineHeader">
    </div>
</div>
<div id="mainBodyItemPage">
    <div id="mainBodyItemPageBlock">
        <table width="450px" border="0" cellspacing="0px" cellpadding="0px">
            <tr>
                <td width="100">
                    <h6>Location:</h6>
                </td>
                <td>
                    <asp:Label ID="lblLocation" runat="server" Text="" Font-Size="14px" />
                </td>
            </tr>
            <tr>
                <td width="100">
                    <h6>Area:</h6>
                </td>
                <td>
                    <h7><asp:Label ID="lblArea" runat="server" Text="" Font-Size="14px" /></h7>
                </td>
            </tr>
            <tr runat="server" id="objContactName">
                <td width="100">
                    <h6>Contact Name:</h6>
                </td>
                <td>
                    <h7><asp:Label ID="lblContactName" runat="server" Text="" Font-Size="14px" ></asp:Label></h7>
                </td>
            </tr>
            <tr runat="server" id="objContactDetail">
                <td width="100">
                    <h6>Contact Detail:</h6>
                </td>
                <td>
                    <asp:Literal ID="litContactDetails" runat="server" />
                </td>
            </tr>
            <tr>
                <td width="100">
                    <h6>Date Listed:</h6>
                </td>
                <td>
                    <h7><asp:Label ID="lblDatePosted" runat="server" Text="" Font-Size="14px"  /></h7>
                </td>
            </tr>
        </table>
    </div>
    <div id="mainBodyItemPageBlock">

        <telerik:RadAjaxLoadingPanel ID="radLoadingPanel" runat="server" Skin="Default" BackgroundPosition="Center" />
        <telerik:RadAjaxManager runat="server" ID="radAjaxManager">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="paramountGallery">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="paramountGallery" LoadingPanelID="radLoadingPanel" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <asp:Panel ID="pnlGallery" runat="server">
            <paramountIt:ImageGallery ID="paramountGallery" runat="server" MainImageHeight="300" MainImageWidth="300" />
        </asp:Panel>
    </div>

    <div style="padding-left: 10px">
        <iframe runat="server" id="onlineAdFrame" frameborder="0" scrolling="no"
            marginheight="10px" marginwidth="10px" width="100%" class="frameWithBody"></iframe>
    </div>

    <div style="margin-left: 10px; display: inline-block; width: 180px; vertical-align: top">
        <a id="twitter" runat="server" href="https://twitter.com/share" class="twitter-share-button" 
            data-text="http://iflog.com.au" data-size="medium">Tweet</a>
        <script type="text/javascript">
            !function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0], p = /^http:/.test(d.location) ? 'http' : 'https'; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = p + '://platform.twitter.com/widgets.js'; fjs.parentNode.insertBefore(js, fjs); } }(document, 'script', 'twitter-wjs');
        </script>
    </div>
    <div id="facebook" runat="server" style="margin-left: 15px; display:inline-block" class="fb-like" 
        data-href="http://iflog.com.au" data-width="450" data-action="recommend" 
        data-show-faces="true" data-send="false"></div>
</div>

