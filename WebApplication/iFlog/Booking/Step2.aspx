<%@ Page Language="vb" MasterPageFile="~/Master/Default.Master" AutoEventWireup="false"
    CodeBehind="Step2.aspx.vb" Inherits="BetterclassifiedsWeb.Step2"
    Title="iFlog - New Booking - Select Category and Publication" %>

<%@ Register Src="~/Controls/Booking/PaperListSelection.ascx" TagName="PaperListSelection" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/Booking/NavigationButtons.ascx" TagName="NavigationButtons" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/ErrorList.ascx" TagName="PageError" TagPrefix="ucx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="clearFloat">
        <div id="contentBodyAccounts">
            <div id="mainBookAd">
                <div class="mainBookingHeader">
                    <h3>Select Category and Publication</h3>
                </div>

                <div id="mainContentMyAccount">
                    <%--Page Error List--%>
                    <div id="Div1">
                        <ucx:PageError ID="ucxPageErrors" runat="server" />

                    </div>
                    <asp:UpdatePanel ID="pnlUpdate" runat="server">
                        <ContentTemplate>
                            <%--Select Category--%>
                            <div id="bookAdMainContent">
                                <table>
                                    <tr>
                                        <td>
                                            <h2>Choose Category</h2>
                                        </td>
                                        <td>
                                            <h5 style="margin-top: 15px;">Please select the appropriate category</h5>
                                        </td>
                                        <td>
                                            <div class="help-context-panel" style="margin-top: 10px;">
                                                <paramountItCommon:HelpContextControl Position="Bottom" ID="helpContextPanel" ImageUrl="~/Resources/Images/question_button.gif"
                                                    runat="server">
                                                    <ContentTemplate>
                                                        <span class="text-wrapper"><b>Choose Category:</b> Select the reason for making the
                                            ad. Example: If you are in a band and need a new guitarist, in the employment option
                                            you would select 'Musicians Wanted' and in administration you would select 'Guitarist'.
                                                        </span>
                                                    </ContentTemplate>
                                                </paramountItCommon:HelpContextControl>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="bookAdMainContent">
                                <table width="520" border="0" cellspacing="0px" cellpadding="0px">
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
                                    <h2>Publication:</h2>
                                    <div class="spacerBookAd">
                                        &nbsp;
                                    </div>
                                    <h5>Please select single or multiple publications.</h5>
                                </div>
                                <div id="bookAdMainContent">
                                    <ucx:PaperListSelection ID="ucxPaperList" runat="server" AllowMultiplePapers="true" />
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <ucx:NavigationButtons ID="ucxNavButtons" runat="server" StepNumber="2" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
