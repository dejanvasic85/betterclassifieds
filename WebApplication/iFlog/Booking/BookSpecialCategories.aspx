<%@ Page Title="iFlog - Free Booking Select Categories" Language="vb" AutoEventWireup="false"
    MasterPageFile="~/Master/Default.Master" CodeBehind="BookSpecialCategories.aspx.vb"
    Inherits="BetterclassifiedsWeb.BookSpecialCategories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="clearFloat">
        <div id="contentBodyAccounts">
            <div id="mainBookAd">
                <div id="mainContentMyAccount">
                    <%--Top Page Description--%>
                    <div class="mainBookingHeader">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/Images/new_ad_header_free.gif"
                            AlternateText="Free Booking" />
                        <h3>
                            <span style="color: #38ACEC">FREE</span> Print and Online Ads!</h3>
                    </div>
                    <paramountIt:ErrorList runat="server" ID="paramountErrors" runat="server" />
                    <div id="bookAdMainContent">
                        <table width="700" border="0" cellspacing="0px" cellpadding="0px">
                            <tr>
                                <td>
                                    <h4>
                                        To ensure that everyone can take advantage of iFlog, we have included no frills
                                        FREE ad space for your use.</h4>
                                </td>
                            </tr>
                            <tr>
                                <td><h4>Not only do you get the same excellent online value as 
                                with our <asp:LinkButton ID="btnUpgrade" runat="server" Text="Premium Offer" />, you can place a 40 word classified in any 
                                of our magazines, completely FREE!</h4></td>
                            </tr>
                            <tr>
                                <td>
                                    <h4>
                                        Please start by choosing your category and sub-category. You will be able to revert
                                        to Premium package at any time.</h4>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%--return to top mark--%>
                    <a id="0" name="0"></a>
                    <%--Category Selection--%>
                    <div id="bookAdMainContent">
                        <h2>
                            <div class="green">
                                Choose Category</div>
                        </h2>
                        <div class="spacerBookAd">
                            &nbsp;</div>
                        <h5>
                            Please select the appropriate catagory and sub-category</h5>
                            
                         <div class="help-context-panel">
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
                    </div>
                    <div id="bookAdMainCategory">
                        <asp:UpdatePanel ID="pnlUpdateCategories" runat="server">
                            <ContentTemplate>
                                <table width="520" border="0" cellspacing="0px" cellpadding="0px">
                                    <tr>
                                        <td width="80">
                                            <h3>
                                                Category:</h3>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlMainCategory" runat="server" Width="150px" DataTextField="Title"
                                                DataValueField="MainCategoryId" AutoPostBack="True" />
                                        </td>
                                        <td width="20">
                                            &nbsp;
                                        </td>
                                        <td width="120">
                                            <h3>
                                                Sub-category:</h3>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlSubCategory" runat="server" Width="150px" DataTextField="Title"
                                                DataValueField="MainCategoryId" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <%--Search Button--%>
                    <div id="bookAdMainCategoryButton" style="margin-bottom: 30px;">
                        <div class="wordcount">
                            <asp:LinkButton ID="btnCreateAd" runat="server" Text="Create Ad" />
                        </div>
                    </div>
                    <div class="spacerBookAdBottom">
                        &nbsp;</div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
