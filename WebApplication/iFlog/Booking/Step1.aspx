<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Default.Master"
    CodeBehind="Step1.aspx.vb" Inherits="BetterclassifiedsWeb.AdType" Title="iFlog - New Booking - Select Package" %>

<%@ Register Src="~/Controls/Booking/AdTypeList.ascx" TagName="AdTypeList" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/Booking/NavigationSteps.ascx" TagName="NavigationSteps"
    TagPrefix="ucx" %>
<%@ Register Src="~/Controls/Booking/NavigationButtons.ascx" TagName="NavigationButtons"
    TagPrefix="ucx" %>
<%@ Register Src="~/Controls/ErrorList.ascx" TagName="PageErrors" TagPrefix="ucx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="clearFloat">
        <div id="contentBodyAccounts">
            <div id="mainBookAd">
                <div class="mainBookingHeader">
                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Resources/Images/new_ad_header.jpg"
                        AlternateText="Premium Booking" />
                    <h3>
                        Select a package</h3>
                </div>
                <asp:Panel ID="pnlBooking" runat="server">
                    <div id="mainContentMyAccount">
                        <div class="bookAdMainContent">
                            <ucx:PageErrors ID="ucxPageErrors" runat="server" />
                            <div style="margin-top: 4px;">
                                <div style="margin-left: 10px;">
                                    <asp:Label ID="lblSessionExpired" runat="server" Text="Your session has expired. Please begin the booking process with a package."
                                        ForeColor="Red" />
                                </div>
                                <%--Adding an extra option for user to select from (redirect to special rates for both)--%>
                                <table width="700px" cellpadding="10" cellspacing="0" align="center">
                                    <tr>
                                        <td width="20" align="right">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/Images/blue_arrow_right.jpg"
                                                AlternateText="blue arrow" />
                                        </td>
                                        <td width="350">
                                            <h4>
                                                <span style="color: #38ACEC">PREMIUM! </span>Bundled Print and Online Ads</h4>
                                                <div class="help-context-panel" style="margin-top:7px">
                                                    <paramountItCommon:HelpContextControl Position="Bottom" ID="HelpContextControl1"
                                                ImageUrl="~/Resources/Images/question_button.gif" runat="server">
                                                <ContentTemplate>
                                                    <span class="text-wrapper"><b >Premium:</b>
                                                        <br />
                                                        <br />
                                                        PRICE: $14.95 per publication (per issue)
                                                        <br />
                                                        WHAT YOU GET:
                                                        <br />
                                                        PRINT: Bold Header, High Quality Image, Unlimited Words.
                                                        <br />
                                                        ONLINE: Bold Header, 2 x High Quality Images, Unlimited Words, 4 Week Placement.</span>
                                                </ContentTemplate>
                                            </paramountItCommon:HelpContextControl>
                                                </div>
                                        </td>
                                        
                                         
                                        <td width="155" valign="top">
                                            <img src="../Resources/Images/Packages/example_premium.gif" alt="Premium" />
                                        </td>
                                        <td width="20" align="center" valign="middle">
                                            <asp:RadioButton ID="rdoPremium" runat="server" GroupName="Package" />
                                        </td>

                                    </tr> 
                                       
                                    <tr>
                                        <%--<td width="20" align="right">
                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Resources/Images/blue_arrow_right.jpg"
                                                AlternateText="blue arrow" />
                                        </td>
                                        <td width="350">
                                            <h4>
                                                <span style="color: #38ACEC">FREE!</span> Bundled Print and Online Ads</h4>
                                       <div class="help-context-panel" style="margin-top:7px">
                                            <paramountItCommon:HelpContextControl Position="Bottom" ID="HelpContextControl2"
                                                ImageUrl="~/Resources/Images/question_button.gif" runat="server">
                                                <ContentTemplate>
                                                    <span class="text-wrapper"><b>FREE:</b><br /> <br />
                                                        PRICE: FREE!
                                                        <br />
                                                        WHAT YOU GET:
                                                        <br />
                                                        PRINT: 40 Words (no Bold Header or High Quality Image).
                                                        <br />
                                                        ONLINE: Bold Header, 2 x High Quality Images, Unlimited Words, 4 Week Placement.
                                                    </span>
                                                </ContentTemplate>
                                            </paramountItCommon:HelpContextControl>
                                        </div> 
                                        </td>
                                        <td width="155" valign="top">
                                            <img src="../Resources/Images/Packages/example_free.gif" alt="Free Package" />
                                        </td>
                                        <td width="20" align="center" valign="middle">
                                            <asp:RadioButton ID="rdoFree" runat="server" GroupName="Package" />
                                        </td>--%>
                                        
                                    </tr>
                                    <tr>
                                        <td width="20" align="right">
                                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Resources/Images/blue_arrow_right.jpg"
                                                AlternateText="blue arrow" />
                                        </td>
                                        <td width="250">
                                            <h4>
                                                Online Only Ads</h4>
                                        <div class="help-context-panel" style="margin-top:7px">
                                            <paramountItCommon:HelpContextControl Position="Bottom" ID="HelpContextControl3"
                                                ImageUrl="~/Resources/Images/question_button.gif" runat="server">
                                                <ContentTemplate>
                                                    <span class="text-wrapper"><b>ONLINE:</b> <br /><br /> PRICE: FREE!<br />
                                                        WHAT YOU GET:<br />
                                                        ONLINE: Bold Header, 2 x High Quality Images, Unlimited Words, 4 Week Placement.
                                                    </span>
                                                </ContentTemplate>
                                            </paramountItCommon:HelpContextControl>
                                        </div>
                                        </td>
                                        <td width="155" valign="top">
                                            <img src="../Resources/Images/Packages/example_online.gif" alt="Online Package" />
                                        </td>
                                        <td width="20" align="center" valign="middle">
                                            <asp:RadioButton ID="rdoOnlineOnly" runat="server" GroupName="Package" />
                                        </td>
                                       
                                    </tr>
                                </table>
                            </div>
                            <asp:Label ID="lblInformation" runat="server" Text=""></asp:Label>
                        </div>
                        <ucx:NavigationButtons ID="ucxNavButton" runat="server" StepNumber="1" />
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
