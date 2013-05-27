<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Default.Master"
    CodeBehind="Step5.aspx.vb" Inherits="BetterclassifiedsWeb.Step5" 
    Title="iFlog - New Booking - Confirmation" %>

<%@ Register Src="~/Controls/Booking/EditionDates.ascx" TagName="EditionDates" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/Booking/NavigationButtons.ascx" TagName="NavigationButtons"
    TagPrefix="ucx" %>
<%@ Register Src="~/Controls/ErrorList.ascx" TagName="ErrorList" TagPrefix="ucx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="clearFloat">
        <div id="contentBodyAccounts">
            <div id="mainBookAd">
                <%--<div id="mainHeaderBookAd">
                    <ucx:NavigationSteps runat="server" ID="nav" StepNumber="5" Instruction="Confirmation and Payment"
                        Description="Confirm details below and provide payment details before checkout. <br />Ensure all confirmation boxes are ticked to proceed." />
                </div>--%>
                <div class="mainBookingHeader">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Resources/Images/new_ad_header_online.gif"
                        AlternateText="Premium Booking" />
                    <h3>Confirmation</h3>
                </div>
                <div id="mainContentMyAccount">
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
                                        and the ad you created will soon appear online.</span>
                                </ContentTemplate>
                            </paramountItCommon:HelpContextControl>
                        </div>
                    </div>
                    <div id="bookAdMainContent">
                        <asp:DataList ID="lstBookingSummary" runat="server" Width="520" BorderStyle="None"
                            CellPadding="5" CellSpacing="0" RepeatColumns="1" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td width="160">
                                            <h3>
                                                Reference No</h3>
                                        </td>
                                        <td width="360">
                                            <h3>
                                                <asp:Label ID="lblRefNumber" runat="server" Text='<%# Eval("BookReference") %>' /></h3>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h3>
                                                Start Date</h3>
                                        </td>
                                        <td>
                                            <h3>
                                                <asp:Label ID="lblStartDate" runat="server" Text='<%# String.Format("{0:D}", Eval("StartDate")) %>' /></h3>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h3>
                                                End Date</h3>
                                        </td>
                                        <td>
                                            <h3>
                                                <asp:Label ID="lblEndDate" runat="server" Text='<%# String.Format("{0:D}", Eval("EndDate")) %>' /></h3>
                                        </td>
                                    </tr>
                                    <% If AdType = BetterclassifiedsCore.SystemAdType.LINE Then%>
                                    <tr>
                                        <td>
                                            <h3>
                                                Single Ad Price</h3>
                                        </td>
                                        <td>
                                            <h3>
                                                <asp:Label ID="lblSinglePrice" runat="server" Text='<%# String.Format("{0:C}", Eval("SingleAdPrice")) %>'></asp:Label></h3>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h3>
                                                Editions</h3>
                                        </td>
                                        <td>
                                            <h3>
                                                <asp:Label ID="lblEditions" runat="server" Text='<%# Eval("Insertions") %>'></asp:Label></h3>
                                        </td>
                                    </tr>
                                    <% End If%>
                                    <tr>
                                        <td>
                                            <h3>
                                                Total Price</h3>
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
                                    <% If AdType = BetterclassifiedsCore.SystemAdType.ONLINE Then%>
                                    <telerik:RadWindow ID="radOnlineWindow" runat="server" Width="650px" Height="500px"
                                        Modal="true" NavigateUrl="../OnlineAds/Preview.aspx?viewType=session" OnClientShow="OnRadWindowLoad"
                                        VisibleStatusbar="false" />
                                    <asp:Button ID="btnPreviewOnline" runat="server" Text="Preview Online Ad" Behaviors="Close" />
                                    <% End If%>
                                    <% If AdType = BetterclassifiedsCore.SystemAdType.LINE Then%>
                                    <telerik:RadWindow ID="radLineWindow" runat="server" Width="650px" Height="500px"
                                        Modal="true" NavigateUrl="../LineAds/PreviewLineAd.aspx?viewType=session"  VisibleStatusbar="false" />
                                    <asp:Button ID="btnPreviewLine" runat="server" Text="Preview Line Ad" Behaviors="Close" />
                                    <% End If%>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <% If AdType = BetterclassifiedsCore.SystemAdType.LINE Then%>
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
                        <ucx:EditionDates ID="ucxEditionDates" runat="server" />
                    </div>
                    <% End If %>
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
                    <asp:Panel runat="server" ID="paymentPanel">
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
                    <ucx:NavigationButtons ID="ucxNavButtons" runat="server" StepNumber="5" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
