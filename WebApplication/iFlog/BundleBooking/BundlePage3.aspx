<%@ Page Title="New Bundle Booking - Design your Ad" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Default.Master"
    CodeBehind="BundlePage3.aspx.vb" Inherits="BetterclassifiedsWeb.BundlePage3" %>

<%@ Register Src="~/Controls/Booking/DesignOnlineAd.ascx" TagName="OnlineAd" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/ErrorList.ascx" TagName="PageErrors" TagPrefix="ucx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var checkRequired = false;

        function imageWindowClosed(sender, eventArgs) {
            checkRequired = true;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="bookingpage-pageContainer">

        <div class="bookingpage-bookingHeaderContainer">
            <div class="bookingpage-bookingHeaderText">
                <h3>Design your Print and Online Ads</h3>
            </div>
        </div>

        <div class="bookingpage-bookingContentWrapper">

            <div style="margin-bottom: 5px;" id="divMarketingContent" runat="server">
                <asp:Panel ID="pnlMarketingContent" runat="server" />
            </div>

            <div class="bookingpage-BookingContent">
                <ucx:PageErrors ID="ucxPageErrors" runat="server" />

                <div class="bookingMainContent" style="width: 500px">
                    <paramountIt:LineAdNewDesign ID="lineAdNewDesign" runat="server"
                        HelpContextImageUrl="~/Resources/Images/question_button.gif" CssClass="lineAdNewDesign"
                        Width="500px"
                        Style="display: block;" />
                </div>

                <div class="bookingMainContent" style="width: 500px">
                    <%--Booking Content--%>
                    <h1>Upload Images 
                    </h1>

                    <div>
                        <table>
                            <tr>
                                <td valign="bottom">
                                    <div style="font-size: 0.8em; border: 2px solid #646D7E; padding: 5px; background-color: #E3E4FA;">
                                        Click button below to upload and manage Print and Online Ad Images.
                                    </div>
                                </td>
                                <td valign="top">
                                    <paramountItCommon:HelpContextControl Position="Top" ID="HelpContextControl1"
                                        ImageUrl="~/Resources/Images/question_button.gif"
                                        runat="server" CssClass="upload-help">
                                        <ContentTemplate>
                                            <span class="text-wrapper">
                                                <b>Image:</b> You have the option of selecting
                                                an image to enhance the appeal of your ad to the public. 
                                            </span>
                                        </ContentTemplate>
                                    </paramountItCommon:HelpContextControl>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="wordcount">
                                        <asp:LinkButton ID="lnkUploadImages" runat="server" Text="Manage" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

                <telerik:RadWindow ID="radWindowImages" runat="server" NavigateUrl="~/Common/UploadBookingManager.aspx"
                    Title="Upload Images" Width="680px" Height="580px" Modal="true" Behaviors="Close"
                    ReloadOnShow="true" VisibleStatusbar="false" OnClientClose="imageWindowClosed">
                </telerik:RadWindow>

                <%--Online Ad Details--%>
                <ucx:OnlineAd ID="ucxOnlineAd" runat="server" />

            </div>

            <div class="bookingpage-BookingSideContent">
                <%--Main Div for Summary Details--%>
                <paramountIt:AdOrderSummary ID="paramountOrderSummary" runat="server"
                    IsFloatingOnPage="false" IsAutoPriceCheck="true"
                    HelpContextImageUrl="~/Resources/Images/shopping_cart_16x16.gif" />
            </div>

            <div class="bookingpage-ButtonNavContainer">
                <div class="bookingpage-ButtonsNav">
                    <ul>
                        <li>
                            <asp:LinkButton ID="btnPrevious" runat="server" Text="BACK" CausesValidation="false" CssClass="btnNav" />
                        </li>
                        <li>
                            <asp:LinkButton ID="btnNext" runat="server" Text="NEXT" CausesValidation="true" />
                        </li>
                    </ul>
                </div>
            </div>
            <div class="spacerBookAdBottom">&nbsp;</div>
            <div class="bookingpage-ButtonsCancel">
                <asp:LinkButton ID="btnCancel" runat="server" Text="CANCEL" PostBackUrl="~/Booking/Default.aspx?action=cancel" />
            </div>
        </div>
    </div>
    
</asp:Content>

