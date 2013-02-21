<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Default.Master"
    CodeBehind="BookSpecial.aspx.vb" Inherits="BetterclassifiedsWeb.BookSpecial"
    Title="iFlog - Free Booking - Design and Schedule" %>

<%@ Register Src="~/Controls/Booking/DesignLineAd.ascx" TagName="DesignLignAd" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/Booking/DesignOnlineAd.ascx" TagName="DesignOnlineAd"
    TagPrefix="ucx" %>
<%@ Register Src="~/Controls/Booking/PaperListSelection.ascx" TagName="Publications"
    TagPrefix="ucx" %>
<%@ Register Src="~/Controls/Booking/EditionDates.ascx" TagName="EditionDates" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/ErrorList.ascx" TagName="ErrorList" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/Booking/AdDetails.ascx" TagName="AdDetails" TagPrefix="ucx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="clearFloat">
        <div id="contentBodyAccounts">
            <div id="mainBookAd">
                <div id="mainContentMyAccount">
                    <div class="mainBookingHeader">
                        <asp:Image ID="ImageHeader" runat="server" ImageUrl="~/Resources/Images/new_ad_header_free.gif"
                            AlternateText="Free Booking" />
                        <h3>
                            Design and Schedule</h3>
                    </div>
                    <asp:Panel ID="fieldSetPublication" runat="server">
                        <div id="bookAdMainContent">
                            <ucx:ErrorList ID="ucxPageErrors" runat="server" />
                        </div>
                        <asp:Panel ID="pnlPublicationSelect" runat="server">
                            <%--Select Publication--%>
                            <div id="bookAdMainContent">
                                <h2>
                                    Publication*:</h2>
                                <div class="spacerBookAd">
                                    &nbsp;</div>
                                <h5>
                                    Please select single or multiple publications.</h5>
                                <div class="help-context-panel">
                                    <paramountItCommon:HelpContextControl Position="Bottom" ID="HelpContextControl1"
                                        ImageUrl="~/Resources/Images/question_button.gif" runat="server">
                                        <ContentTemplate>
                                            <span class="text-wrapper">
                                                <p>
                                                    Pick a magazine to suit your ad.
                                                </p>
                                                <br />
                                                <b>3D World:</b> 3D World is Sydney's largest free weekly dance magazine. With 30,000
                                                copies distributed throughout Sydney, Newscastle, Wollongong and Canberra.
                                                <br />
                                                <b>Drum Media:</b> Drum Media is Sydney's largest free weekly magazine aimed at
                                                18-35 year olds. Over 34,000 copies are distributed throughout Sydney, Newscastle,
                                                Wollongong and Canberra. </span>
                                        </ContentTemplate>
                                    </paramountItCommon:HelpContextControl>
                                </div>
                            </div>
                            <div id="bookAdMainContent">
                                <ucx:Publications ID="ucxPublications" runat="server" AllowMultiplePapers="true" />
                            </div>
                        </asp:Panel>
                    </asp:Panel>
                    <%--Image Uploader--%>
                    <div id="bookAdMainContent">
                        <h1>
                            Upload Images</h1>
                        <div style="margin-top: 20px" class="help-context-panel">
                            <paramountItCommon:HelpContextControl Position="Bottom" ID="HelpContextControl2"
                                ImageUrl="~/Resources/Images/question_button.gif" runat="server">
                                <ContentTemplate>
                                    <span class="text-wrapper"><b>Image:</b> You have the option of selecting an image to
                                        enhance the appeal of your ad to the public. </span>
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
                    <telerik:RadWindow ID="radWindowImages" runat="server" NavigateUrl="~/Common/UploadBookingManager.aspx"
                        Title="Upload Images" Width="680px" Height="580px" Behaviors="Close" Modal="true" ReloadOnShow="true" VisibleStatusbar="false" />
                    <br />
                    <ucx:DesignLignAd ID="ucxLineAd" runat="server" Visible="false" ShowWordCount="true" />
                    <ucx:DesignOnlineAd ID="ucxOnlineAd" runat="server" Visible="false" />
                    
                    <br />
                    <%--Scheduling Details--%>
                    <asp:UpdatePanel ID="pnlUpdateDate" runat="server">
                        <ContentTemplate>
                            <div id="bookAdMainContent">
                                <h2>
                                    Start Date*</h2>
                                 <div class="help-context-panel" style="margin-top:15px">
                                    <paramountItCommon:HelpContextControl Position="Bottom" ID="helpContextPanel" ImageUrl="~/Resources/Images/question_button.gif"
                                        runat="server">
                                        <ContentTemplate>
                                            <span class="text-wrapper"><b>Start Date:</b> Press on one of the dates that is highlighted
                                                and your ad will run for a week from the date selected in print and online.
                                            </span>
                                        </ContentTemplate>
                                    </paramountItCommon:HelpContextControl>
                                </div>
                                <h5>
                                    <asp:Label ID="lblDateInformation" runat="server" /></h5>
                             
                            </div>
                            <div id="bookAdMainContent">
                                <asp:Calendar ID="calStartDate" runat="server" BackColor="White" BorderColor="#53c0e7"
                                    CellPadding="0" BorderWidth="0px" DayNameFormat="Shortest" Font-Names="Verdana"
                                    Font-Size="8pt" ForeColor="#003399" Height="250px" Width="270px" FirstDayOfWeek="Monday">
                                    <SelectedDayStyle BackColor="#53C0E7" Font-Bold="True" ForeColor="Black" />
                                    <SelectorStyle BackColor="#99CCCC" ForeColor="White" />
                                    <OtherMonthDayStyle ForeColor="#999999" />
                                    <NextPrevStyle Font-Size="10pt" Font-Bold="true" ForeColor="#CCCCFF" CssClass="calNavStyle" />
                                    <DayHeaderStyle BackColor="#367D8F" ForeColor="White" Height="20px" />
                                    <TitleStyle CssClass="calHeadStyle" Height="28px" Font-Bold="True" ForeColor="White" />
                                    <DayStyle CssClass="calDayStyle" />
                                </asp:Calendar>
                            </div>
                            <div id="bookAdMainContent">
                                <asp:Panel ID="tdLineAd" runat="server" CssClass="verify">
                                    <asp:LinkButton ID="lnkCheckEditions" runat="server" Text="Check Editions" />
                                </asp:Panel>
                            </div>
                            <asp:Panel ID="pnlEditions" runat="server" CssClass="modalPopup" Width="600px">
                                <ucx:EditionDates ID="ucxEditionDates" runat="server" />
                                <div align="center">
                                    <asp:Button ID="btnClose" runat="server" Text="Close" /></div>
                            </asp:Panel>
                            <ajax:ModalPopupExtender ID="modalPopup1" runat="server" BackgroundCssClass="modalBackground"
                                CancelControlID="btnClose" PopupControlID="pnlEditions" TargetControlID="lnkCheckEditions" />
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <%--Update price not available in free edition--%>
                    <%--<asp:UpdateProgress ID="UpdateProgressPanel" runat="server" AssociatedUpdatePanelID="pnlUpdateDate">
                        <ProgressTemplate>
                            <div id="bookAdMainContent">
                                <table>
                                    <tr>
                                        <td align="center">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/Images/indicator.gif"
                                                Height="16" Width="16" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblProgress" runat="server" Text="Please Wait" CssClass="progressWait" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>--%>
                    <div id="myAccountTableButtonsPw" style="width: 900px;">
                        <ul>
                            <div id="myAccountTableButtonsCancel">
                                <li>
                                    <asp:LinkButton ID="lnkCancel" runat="server" Text="CANCEL" PostBackUrl="~/Default.aspx" />
                                </li>
                            </div>
                            <div id="myAccountTableButtonsExtend">
                                <li>
                                    <asp:LinkButton ID="btnContinue" runat="server" Text="NEXT" />
                                </li>
                            </div>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
