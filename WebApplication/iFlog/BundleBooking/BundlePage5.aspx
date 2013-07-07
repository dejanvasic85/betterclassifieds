<%@ Page Title="iFlog - New Bundle Booking - Confirmation" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Default.Master"
    CodeBehind="BundlePage5.aspx.vb" Inherits="BetterclassifiedsWeb.BundlePage5" %>

<%@ Register Src="~/Controls/Booking/EditionDates.ascx" TagName="EditionDates" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/ErrorList.ascx" TagName="errorlist" TagPrefix="ucx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="clearFloat">
        <div id="contentBodyAccounts">
            <div id="mainBookAd">
               
                <div class="mainBookingHeader">
                    
                    <h3>Confirmation</h3>
                </div>
                <div id="mainContentMyAccount">
                    <%--Error List--%>
                    <div id="bookAdMainContent">
                        <ucx:errorlist ID="ucxErrorList" runat="server" />
                    </div>
                    <div id="bookAdMainContent">
                        
                        <div style="width: 255px; display: inline-block;  vertical-align: top; padding-right: 20px;">
                            <paramountIt:AdOrderSummary ID="paramountOrderSummary" runat="server" 
                                IsFloatingOnPage="false" IsAutoPriceCheck="false" 
                                HelpContextImageUrl="~/Resources/Images/shopping_cart_16x16.gif" />
                        </div>

                        <div style="width: 350px; display: inline-block; vertical-align: top;">
                        
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
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                          
                            <table style="margin-top: 10px;">
                                <tr>
                                    <td>
                                        <telerik:RadWindow ID="radOnlineWindow" runat="server" Width="650px" Height="500px"
                                            Modal="true" NavigateUrl="../OnlineAds/Preview.aspx?viewType=session" Behaviors="Close" VisibleStatusbar="false" />
                                        <asp:Button ID="btnPreviewOnline" runat="server" Text="Preview Online Ad" />
                                        <telerik:RadWindow ID="radLineWindow" runat="server" Width="240px" Height="400px"
                                            Modal="true" NavigateUrl="../LineAds/PreviewLineAd.aspx?viewType=session" />
                                        <asp:Button ID="btnPreviewLine" runat="server" Text="Preview Line Ad" Behaviors="Close" VisibleStatusbar="false" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                                                
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
                        <ucx:EditionDates ID="ucxEditionDates" runat="server" />
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
                    <%--Navigation Buttons--%>
                    <div id="myAccountTableButtonsAll">
                        <div id="myAccountTableButtonsExtend" style="float: right;">
                            <ul>
                                <li>
                                    <asp:LinkButton ID="btnPrevious" runat="server" Text="BACK" CausesValidation="false"
                                        CssClass="btnNav" /></li>
                                <li>
                                    <asp:LinkButton ID="btnNext" runat="server" Text="NEXT" CausesValidation="true" /></li>
                            </ul>
                        </div>
                    </div>
                    <div class="spacerBookAdBottom">
                        &nbsp;</div>
                    <div id="myAccountTableButtonsRight">
                        <div id="myAccountTableButtonsCancel">
                            <ul>
                                <li>
                                    <asp:LinkButton ID="btnCancelBooking" runat="server" Text="CANCEL" PostBackUrl="~/Booking/Default.aspx?action=cancel" />
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
