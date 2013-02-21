<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Default.Master"
    CodeBehind="Step3.aspx.vb" Inherits="BetterclassifiedsWeb.Step3" 
    Title="iFlog - New Booking - Design Ad" %>

<%@ Register Src="~/Controls/Booking/DesignLineAd.ascx" TagName="DesignLineAd" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/Booking/DesignOnlineAd.ascx" TagName="DesignOnlineAd"
    TagPrefix="ucx" %>
<%@ Register Src="~/Controls/Booking/AdDetails.ascx" TagName="AdDetails" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/Booking/PriceSummary.ascx" TagName="PriceSummary" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/Booking/NavigationSteps.ascx" TagName="NavigationSteps"
    TagPrefix="ucx" %>
<%@ Register Src="~/Controls/Booking/NavigationButtons.ascx" TagName="NavigationButtons"
    TagPrefix="ucx" %>
<%@ Register Src="~/Controls/ErrorList.ascx" TagName="PageErrors" TagPrefix="ucx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="clearFloat">
        <div id="contentBodyAccounts">
            <div id="mainBookAd">
                <%--<div id="mainHeaderBookAd">
                    <ucx:NavigationSteps runat="server" ID="nav" StepNumber="3" Instruction="Design your Print and Online Ads"
                        Description="Manage ad details first, then upload images. Finally, click on the Update Price and then Summary details to find the total cost of a single edition." />
                </div>--%>
                <div class="mainBookingHeader">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/Images/new_ad_header_online.gif"
                        AlternateText="Premium Booking" />
                    <h3>Design your Print and Online Ads</h3>
                </div>
                <div id="mainContentMyAccount">
                    <%--Page Error List--%>
                    <div id="bookAdMainContent">
                        <ucx:PageErrors ID="ucxPageErrors1" runat="server" />
                    </div>
                    <%--Ad Title and Comments
                <ucx:AdDetails ID="ucxAdDetails" runat="server" Visible="true" />--%>
                    <div id="bookAdMainContent">                        
                        <h1>
                            Upload Images</h1>
                            <div class="help-context-panel">
                            <paramountItCommon:HelpContextControl Position="Bottom" ID="helpContextPanel" ImageUrl="~/Resources/Images/question_button.gif"
                                runat="server" CssClass="upload-help">
                                <ContentTemplate>
                                    <span class="text-wrapper">
                                    <b>Image:</b>
                                     You have the option of selecting
                                        an image to enhance the appeal of your ad to the public. 
                                        </span>
                                </ContentTemplate>
                            </paramountItCommon:HelpContextControl>
                        </div>
                    </div>
                    <div id="bookAdMainContent">
                        <asp:Label ID="lblInfoImages" runat="server" CssClass="message-info-blue" Text="Use our new upload manager to submit your images. You can always open this window to add and remove at any time." /><br />
                        <br />
                        <div class="wordcount">
                            <asp:LinkButton ID="lnkUploadImages" runat="server" Text="Upload" /></div>
                    </div>
                    <%--Telerik RadWindow for displaying and managing online ad images--%>
                    <telerik:RadWindow ID="radWindowImages" runat="server" NavigateUrl="~/Common/UploadBookingManager.aspx"
                        Title="Upload Images" Width="680px" Height="580px" Behaviors="Close" Modal="true" ReloadOnShow="true" VisibleStatusbar="false" />
                    <ucx:DesignLineAd ID="ucxLineAdDesign" runat="server" Visible="false" ShowWordCount="true" />
                    <ucx:DesignOnlineAd ID="ucxDesignOnlineAd" runat="server" Visible="false" />
                    <br />
                    <br />
                <%--    <div id="bookAdMainContent">
                        <asp:UpdatePanel ID="updatePanel" runat="server">
                            <ContentTemplate>
                                <table width="520" border="0" cellspacing="0px" cellpadding="0px">
                                    <tr>
                                        <td width="277">
                                            &nbsp;
                                        </td>
                                        <td width="114">
                                            <div class="wordcount">
                                                <asp:LinkButton ID="btnUpdatePrice" runat="server" Text="Update Price" />&nbsp;</div>
                                        </td>
                                        <td width="50">
                                            <p>
                                                <asp:LinkButton ID="lnkShowPrice" runat="server" Text="$" /></p>
                                        </td>
                                        <td width="89">
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="pnlPriceDetails" runat="server" CssClass="modalPopup">
                                    <ucx:PriceSummary ID="ucxPriceSummary" runat="server" />
                                    <div align="center">
                                        <asp:Button ID="btnCloseSummary" runat="server" Text="Close" /></div>
                                </asp:Panel>
                                <ajax:ModalPopupExtender ID="modalPopup1" runat="server" BackgroundCssClass="modalBackground"
                                    CancelControlID="btnCloseSummary" PopupControlID="pnlPriceDetails" TargetControlID="lnkShowPrice" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                        <asp:UpdateProgress ID="pnlProgressUpdatePrice" runat="server" AssociatedUpdatePanelID="updatePanel">
                            <ProgressTemplate>
                                <asp:Image ID="imgProgress" runat="server" ImageUrl="~/Resources/Images/indicator.gif" />
                                &nbsp; &nbsp; Please Wait...
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>--%>
                    <%--Bottom Navigation Buttons--%>
                    <ucx:NavigationButtons ID="ucxNavButtons" runat="server" StepNumber="3" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
