<%@ Page Title="New Bundle Booking - Select Category and Publication" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Default.Master"
    CodeBehind="BundlePage2.aspx.vb" Inherits="BetterclassifiedsWeb.BundlePage2" %>

<%@ Register Src="~/Controls/ErrorList.ascx" TagName="PageError" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/Booking/PaperListSelection.ascx" TagName="PaperList"
    TagPrefix="ucx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="clearFloat">
        <div id="contentBodyAccounts">
            <div id="mainBookAd">
                <div class="mainBookingHeader">
                    <h3>Select Category and Publication</h3>
                </div>

                <div id="mainContentMyAccount">
                    <div id="bookAdMainContent">
                        <ucx:PageError ID="ucxPageError" runat="server" />
                    </div>
                    <asp:UpdatePanel ID="pnlUpdate" runat="server">
                        <ContentTemplate>
                            <%--Select Category--%>
                            <div id="bookAdMainContent">
                                <table>
                                    <tr>
                                        <td>
                                            <h2>Choose Category:</h2>
                                        </td>
                                        <td style="vertical-align: middle">
                                            <h5 style="margin-top: 15px;">Please select the appropriate category</h5>
                                        </td>
                                        <td>
                                            <div class="help-context-panel" style="margin-top: 10px">
                                                <paramountItCommon:HelpContextControl Position="Bottom" ID="HelpContextControl3" ImageUrl="~/Resources/Images/question_button.gif"
                                                    runat="server">
                                                    <ContentTemplate>
                                                        <span class="text-wrapper"><b>Choose Category:</b>
                                                            <br />
                                                            Select the reason for making the ad. Example: If you are in a band and need a new
                                                        guitarist, in the employment option you would select 'Musicians Wanted' and in administration
                                                        you would select 'Guitarist'. </span>
                                                    </ContentTemplate>
                                                </paramountItCommon:HelpContextControl>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="bookAdMainContent">
                                <table width="550" border="0" cellspacing="0px" cellpadding="0px">
                                    <tr>
                                        <td width="270">
                                            <asp:DropDownList ID="ddlMainCategory" runat="server" Width="250px" DataTextField="Title"
                                                DataValueField="MainCategoryId" AutoPostBack="true" />
                                        </td>
                                        <td width="250">
                                            <asp:DropDownList ID="ddlSubCategory" runat="server" Width="250px" DataTextField="Title"
                                                DataValueField="MainCategoryId" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <%--Select Publication--%>
                            <asp:Panel ID="pnlPublications" runat="server">
                                <div id="bookAdMainContent">
                                    <table>
                                        <tr>
                                            <td>
                                                <h2>Publication:</h2>
                                            </td>
                                            <td>
                                                <h5 style="margin-top: 15px;">Please select single or multiple publications.</h5>
                                            </td>
                                        </tr>
                                    </table>


                                </div>
                                <div id="bookAdMainContent">
                                    <ucx:PaperList ID="ucxPaperList" runat="server" AllowMultiplePapers="true" />
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
                        &nbsp;
                    </div>
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
