<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Default.Master"
    CodeBehind="BookSpecialConfirm.aspx.vb" Inherits="BetterclassifiedsWeb.BookSpecialConfirm"
    Title="iFlog - Free Booking Confirmation" %>

<%@ Register Src="~/Controls/Booking/EditionDates.ascx" TagName="EditionDates" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/LineAdPreview.ascx" TagName="LineAdView" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/OnlineAdView.ascx" TagName="OnlineAdView" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/ErrorList.ascx" TagName="ErrorList" TagPrefix="ucx" %>
<%@ Import Namespace="BetterclassifiedsCore" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="clearFloat">
        <div id="contentBodyAccounts">
            <div id="mainBookAd">
                <div id="mainContentMyAccount">
                    <div class="mainBookingHeader">
                        <asp:Image ID="ImageHeader" runat="server" ImageUrl="~/Resources/Images/new_ad_header_free.gif"
                            AlternateText="Free Booking" />
                        <h3>
                            Confirmation</h3>
                    </div>
                    <%--Error List--%>
                    <div id="bookAdMainContent">
                        <ucx:ErrorList ID="ucxErrorList" runat="server" />
                    </div>
                    <%--Order Summary Details--%>
                    <div id="bookAdMainContent">
                        <h2>
                            Order Summary</h2>
                        <div class="spacerBookAd">
                            &nbsp;</div>
                        <h5>
                            Important information regarding your booking.</h5>
                        <div class="help-context-panel">
                            <paramountItCommon:HelpContextControl Position="Bottom" ID="helpContextPanel" ImageUrl="~/Resources/Images/question_button.gif"
                                runat="server">
                                <ContentTemplate>
                                    <span class="text-wrapper">Simply confirm the details below, select your payment option
                                        and the ad you created will soon appear online and in print.</span>
                                </ContentTemplate>
                            </paramountItCommon:HelpContextControl>
                        </div>
                    </div>
                    <div id="bookAdMainContent">
                        <asp:DataList ID="listSpecialBookingSummary" runat="server" Width="520" BorderStyle="None"
                            CellPadding="5" CellSpacing="0" RepeatColumns="1" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td width="160">
                                            <h3>
                                                <asp:Label ID="lblMainCategoryLabel" runat="server" Text="Main Category" />:</h3>
                                        </td>
                                        <td width="360">
                                            <h3>
                                                <asp:Label ID="lblMainCategory" runat="server" Text='<%# Eval("MainCategory") %>' /></h3>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h3>
                                                <asp:Label ID="lblRefNumberLabel" runat="server" Text="Reference No" />:</h3>
                                        </td>
                                        <td>
                                            <h3>
                                                <asp:Label ID="lblRefNumber" runat="server" Text='<%# Eval("BookReference") %>' /></h3>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h3>
                                                <asp:Label ID="lblStartDateLbl" runat="server" Text="Start Date" />:</h3>
                                        </td>
                                        <td>
                                            <h3>
                                                <asp:Label ID="lblStartDate" runat="server" Text='<%# String.Format("{0:D}", Eval("StartDate")) %>' /></h3>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h3>
                                                <asp:Label ID="lblEndDateLbl" runat="server" Text="End Date" />:</h3>
                                        </td>
                                        <td>
                                            <h3>
                                                <asp:Label ID="lblEndDate" runat="server" Text='<%# String.Format("{0:D}", Eval("EndDate")) %>' /></h3>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h3>
                                                <asp:Label ID="lblTotalPriceLbl" runat="server" Text="Total Price" />:</h3>
                                        </td>
                                        <td>
                                            <h3>
                                                <asp:Label ID="lblTotalPrice" runat="server" Text='<%# String.Format("{0:C}", Eval("TotalPrice")) %>' /></h3>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                        <table>
                            <tr>
                                <td>
                                    <% If BookingController.SpecialBookCart.IsOnlineAd Then%>
                                    <telerik:RadWindow ID="radOnlineWindow" runat="server" Width="650px" Height="500px"
                                        Modal="true" NavigateUrl="../OnlineAds/Preview.aspx?viewType=session" />
                                    <asp:Button ID="btnPreviewOnline" runat="server" Text="Preview Online Ad" Behaviors="Close" />
                                    <% End If%>
                                    <% If BookingController.SpecialBookCart.IsLineAd Then%>
                                    <telerik:RadWindow ID="radLineWindow" runat="server" Width="650px" Height="500px"
                                        Modal="true" NavigateUrl="../LineAds/PreviewLineAd.aspx?viewType=session" />
                                    <asp:Button ID="btnPreviewLine" runat="server" Text="Preview Line Ad" Behaviors="Close" />
                                    <% End If%>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%--Edition Dates--%>
                    <div id="bookAdMainContent">
                        <h2>
                            Edition Dates:</h2>
                        <div class="spacerBookAd">
                            &nbsp;</div>
                        <h5>
                            When your advertisement will appear and in which magazine/s.</h5>
                    </div>
                    <div id="bookAdMainContent">
                        <ucx:EditionDates ID="ucxEditionDates" runat="server" Visible="false" />
                    </div>
                    <%--Confirmation --%>
                    <div id="bookAdMainContent">
                        <h2>
                            <span class="red">Confirmation</span></h2>
                        <div class="spacerBookAd">
                            &nbsp;</div>
                        <h5>
                            <span class="red">Please confirm information is correct.</span></h5>
                    </div>
                    <div id="bookAdMainContent">
                        <div class="redBorder">
                            <table width="520" border="0" cellspacing="5px" cellpadding="0px">
                                <tr>
                                    <td width="20">
                                        <asp:Image ID="imgArrow1" runat="server" ImageUrl="~/Resources/Images/red_arrow_small_right.gif"
                                            AlternateText="warning arrow" />
                                    </td>
                                    <td width="480">
                                        <h5>
                                            <asp:CheckBox ID="chkConfirm" runat="server" Text="Details Confirmed" /></h5>
                                    </td>
                                    <td width="20">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td width="20">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/Images/red_arrow_small_right.gif"
                                            AlternateText="warning arrow" />
                                    </td>
                                    <td width="480">
                                        <h5>
                                            <asp:CheckBox ID="chkConditions" runat="server" Text="I have read and agreed to the <a href='../Terms.aspx' target='blank'>terms and conditions</a>." /></h5>
                                    </td>
                                    <td width="20">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <%--Payment Option--%>
                    <asp:Panel ID="paymentPanel" runat="server">
                        <div id="bookAdMainContent">
                            <h2>
                                Payment Option</h2>
                            <div class="spacerBookAd">
                                &nbsp;</div>
                            <h5>
                                Select your method of payment.</h5>
                        </div>
                        <div id="bookAdMainContent">
                            <asp:RadioButtonList ID="paymentOption" runat="server" RepeatLayout="Flow">
                                <asp:ListItem Selected="True" Value="cc" Text="Credit Card"></asp:ListItem>
                                <asp:ListItem Text="PayPal" Value="pp"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </asp:Panel>
                    <div id="bookAdMainContent">
                        &nbsp;
                    </div>
                    <div id="myAccountTableButtonsPw" style="width: 900px;">
                        <ul>
                            <div id="myAccountTableButtonsCancel">
                                <li>
                                    <asp:LinkButton ID="lnkCancel" runat="server" Text="CANCEL" PostBackUrl="~/Default.aspx" />
                                </li>
                            </div>
                            <div id="myAccountTableButtonsExtend">
                                <li>
                                    <asp:LinkButton ID="btnPlaceAd" runat="server" Text="SUBMIT" />
                                </li>
                            </div>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
