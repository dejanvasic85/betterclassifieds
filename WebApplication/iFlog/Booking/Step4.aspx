<%@ Page Title="iFlog - New Booking - Scheduling" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/Master/Default.Master"
    CodeBehind="Step4.aspx.vb" Inherits="BetterclassifiedsWeb.Step4" %>

<%@ Register Src="~/Controls/Booking/EditionDates.ascx" TagName="EditionDates" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/Booking/NavigationButtons.ascx" TagName="NavigationButtons" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/ErrorList.ascx" TagName="PageErrors" TagPrefix="ucx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="clearFloat">
        <div id="contentBodyAccounts">
            <div id="mainBookAd">
           
                <div class="mainBookingHeader">
                    <h3>Scheduling Details</h3>
                </div>
                <div id="mainContentMyAccount">
                    <%--Error List--%>
                    <div id="bookAdMainContent">
                        <ucx:PageErrors ID="ucxPageErrors" runat="server" />
                    </div>
                    <asp:UpdateProgress ID="UpdateProgressPanel" runat="server" AssociatedUpdatePanelID="updatePanel"
                        DisplayAfter="2000">
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
                    </asp:UpdateProgress>
                    <asp:UpdatePanel ID="updatePanel" runat="server">
                        <ContentTemplate>
                            <div id="bookAdMainContent">
                            
                                <h2>
                                    Start Date*</h2>
                                <div class="spacerBookAd">
                                    &nbsp;</div>
                                <h5>
                                    Please choose when you would like your ad to appear.</h5>
                                    <div class="help-context-panel">
                                    <paramountItCommon:HelpContextControl Position="Bottom" ID="helpContextPanel" ImageUrl="~/Resources/Images/question_button.gif"
                                        runat="server">
                                        <ContentTemplate>
                                            <span class="text-wrapper"><b>Start Date: </b>Press on one of the dates that is highlighted
                                                and your ad will run for 30 days from that date.</span>
                                        </ContentTemplate>
                                    </paramountItCommon:HelpContextControl>
                                </div>
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
                            <%  If AdType = BetterclassifiedsCore.SystemAdType.LINE Then %>
                            <div id="bookAdMainContent" style="font-size: 11px;">
                                Start Date:
                                <asp:Label ID="lblLineStartDate" runat="server" Font-Bold="true" />
                            </div>
                            <%--Number of Insertions--%>
                            <div id="bookAdMainContent">
                                <h2>
                                    Insertions*</h2>
                                <div class="spacerBookAd">
                                    &nbsp;</div>
                                <h5>
                                    Number of editions you would like your advertisement to run for each publication.</h5>
                            </div>
                            <div id="bookAdMainContent">
                                <table width="520" border="0" cellspacing="0px" cellpadding="0px">
                                    <tr>
                                        <td width="90">
                                            <asp:DropDownList ID="ddlInserts" runat="server" AutoPostBack="true" Width="115px" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <% ElseIf AdType = BetterclassifiedsCore.SystemAdType.ONLINE Then %>
                            <%--End Date--%>
                            <asp:Panel ID="trEndDate" runat="server">
                                <div id="bookAdMainContent">
                                    <h2>
                                        Days Running</h2>
                                    <div class="spacerBookAd">
                                        &nbsp;</div>
                                    <h5>
                                        Online Ads run for 30 days starting from the start date.</h5>
                                </div>
                                <div id="bookAdMainContent">
                                    Start Date:
                                    <asp:Label ID="lblStartDate" runat="server" Text="" Font-Bold="true" />
                                </div>
                                <div id="bookAdMainContent">
                                    End Date:
                                    <asp:Label ID="lblEndDate" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </div>
                            </asp:Panel>
                            <%  End If%>
                            <% If AdType = BetterclassifiedsCore.SystemAdType.LINE Then%>
                            <%--Verify Publication Dates Button--%>
                            <div id="bookAdMainContent">
                                <table width="520" border="0" cellspacing="0px" cellpadding="0px">
                                    <tr>
                                        <td width="327">
                                            &nbsp;
                                        </td>
                                        <td width="193">
                                            <div class="verify">
                                                <asp:LinkButton ID="lnkCheckEditions" runat="server" Text="View Editions" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlEditions" runat="server" CssClass="modalPopup" Width="450px">
                                        <ucx:EditionDates ID="ucxEditionDates" runat="server" />
                                        <div align="center" style="padding: 5px;">
                                            <asp:Button ID="btnClose" runat="server" Text="Close" /></div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <ajax:ModalPopupExtender ID="modalPopup1" runat="server" BackgroundCssClass="modalBackground"
                                CancelControlID="btnClose" PopupControlID="pnlEditions" TargetControlID="lnkCheckEditions" />
                            <%--List of publication deadlines--%>
                            <div id="bookAdMainContent">
                                <h2>
                                    Deadlines</h2>
                                <div class="spacerBookAd">
                                    &nbsp;</div>
                                <h5>
                                    Advertisements must be placed by this day and time.</h5>
                            </div>
                            <div id="bookAdMainContent">
                                <asp:DataList ID="listPublicationDeadlines" runat="server" Width="520px" CellPadding="0"
                                    CellSpacing="0" BorderStyle="None" ItemStyle-Width="520" GridLines="None" RepeatDirection="Horizontal"
                                    RepeatColumns="1">
                                    <ItemTemplate>
                                        <h3>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("Title") %>' Font-Bold="true"></asp:Label></h3>
                                        <h6>
                                            <div class="red">
                                                <asp:Label ID="Label4" runat="server" Text='<%# String.Format("{0:f}", Eval("Deadline")) %>'></asp:Label>
                                            </div>
                                        </h6>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                            <% End If%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <%--Navigation Buttons--%>
                    <ucx:NavigationButtons ID="ucxNavButtons" runat="server" StepNumber="4" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
