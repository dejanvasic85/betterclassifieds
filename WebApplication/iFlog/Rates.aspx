﻿<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MasterWithRightBar.Master"
    CodeBehind="Rates.aspx.vb" Inherits="BetterclassifiedsWeb.Rates" Title="Classies Price List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <style type="text/css">
        .priceSection { display: block; font-size: 12px; }
        .priceTagLine { display: block; padding-bottom: 4px; font-style: italic; font-size: 13px; }
        .priceHeaderTitle { width: 175px; display:inline-block; margin-left: 10px; font-size: 12px; font-family: Tahoma;}
        .priceLabel { display: inline-block; font-family: Tahoma;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div id="main">
        <div id="mainBookAd" style="margin-top: 0px; background: #fff; margin-left: 0px;">
            <div id="mainContentMyAccount">
                <%--Top Page Description--%>
                <div id="bookAdMainContent">
                    <table width="700" border="0" cellspacing="0px" cellpadding="0px">
                        <tr>
                            <td>
                                <h1>
                                    iFlog Publication and Category Rates!</h1>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <h4>
                                    Simply choose your category and sub-category, and we will show you what current
                                    offers you can choose from...</h4>
                            </td>
                        </tr>
                    </table>
                </div>
                
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
                                        <asp:DropDownList ID="ddlMainCategory" runat="server" Width="150px"
                                            DataTextField="Title" DataValueField="MainCategoryId" AutoPostBack="True" />
                                    </td>
                                    <td width="20">
                                        &nbsp;
                                    </td>
                                    <td width="120">
                                        <h3>
                                            Sub-category:</h3>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSubCategory" runat="server" Width="150px"
                                            DataTextField="Title" DataValueField="MainCategoryId" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                
                <%--Search Button--%>
                <div id="bookAdMainCategoryButton">
                    <div class="wordcount">
                        <asp:LinkButton ID="lnkSearch" runat="server" Text="List Rates" />
                    </div>
                </div>
                <asp:Panel ID="pnlNoSpecial" runat="server" Visible="false">
                    <div id="bookAdMainContent">
                        <p>
                            <asp:Label ID="lblNoSpecial" runat="server" Text="There are no special rates for the selected category."
                                ForeColor="Red" /></p>
                    </div>
                </asp:Panel>
                
                <asp:Panel ID="pnlRates" runat="server" Visible="false">
                    <div id="bookAdMainContent">
                        <asp:DataList ID="lstCasualRates" runat="server">
                            <SeparatorTemplate>
                                <div class="spacerSpecial"></div>
                            </SeparatorTemplate>
                            
                            <ItemStyle Width="300px" />
                            
                            <ItemTemplate>
                                
                                <div class="specialOfferTitle">
                                    <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("PublicationName") %>' />
                                </div>

                                <asp:Panel ID="pnlOnlineTemplate" runat="server" Visible='<%# Eval("IsOnlineRate") %>'>
                                    <div class="priceSection">
                                        <asp:Label ID="lblOnlineCost" runat="server" CssClass="priceTagLine"
                                                Text='<%# String.Format("30 Day listing or Bundle with Print for {0}!", Eval("OnlinePriceDescription")) %>' />
                                    </div>
                                </asp:Panel>
                                
                                <asp:Panel ID="pnlPrintLineAdTemplate" runat="server" Visible='<%# Eval("IsPrintLineAdRate") %>'>
                                    <div id="freeWords" class="priceSection" runat="server" visible='<%# Eval("IsFirstWordsFree") %>'>
                                        <asp:Label ID="lblFirstFreeWords" runat="server" CssClass="priceTagLine"
                                            Text='<%# String.Format("First {0} words FREE!", Eval("LineAdFreeUnits")) %>' />
                                    </div>
                                    <div id="minCharge" class="priceSection" runat="server" visible='<%# Eval("MinimumAmountHasValue") %>'>
                                        <div class="priceHeaderTitle">Minimum Charge:</div>
                                        <asp:Label ID="lblStartingPrice" runat="server" CssClass="priceLabel" 
                                            Text='<%# String.Format("{0:C} ", Eval("MinimumAmount")) %>' />
                                    </div>
                                    <div id="wordRate" class="priceSection" runat="server" visible='<%# Eval("LineAdUnitAmountHasValue") %>'>
                                        <div class="priceHeaderTitle">Rate per word:</div>
                                        <asp:Label ID="lblUnitCost" runat="server" CssClass="priceLabel" 
                                            Text='<%# String.Format("{0:C} ", Eval("LineAdUnitAmount")) %>' />
                                    </div>
                                    <div id="photoRate" class="priceSection" runat="server">
                                        <div class="priceHeaderTitle">Print Image:</div>
                                        <asp:Label ID="lblPhotoCost" runat="server" CssClass="priceLabel" 
                                            Text='<%# String.Format("{0:C} ", Eval("LineAdMainPhotoAmount")) %>' />
                                    </div>
                                    <div id="boldheadingRate" class="priceSection" runat="server">
                                        <div class="priceHeaderTitle">Bold Heading:</div>
                                        <asp:Label ID="Label1" runat="server" CssClass="priceLabel" 
                                            Text='<%# String.Format("{0:C} ", Eval("LineAdHeaderAmount")) %>' />
                                    </div>
                                    <div id="superBoldHeadingRate" class="priceSection" runat="server">
                                        <div class="priceHeaderTitle">Super Bold Heading:</div>
                                        <asp:Label ID="Label2" runat="server" CssClass="priceLabel" 
                                            Text='<%# String.Format("{0:C} ", Eval("LineAdSuperBoldHeaderAmount")) %>' />
                                    </div>
                                    <div id="colorHeadingRate" class="priceSection" runat="server">
                                        <div class="priceHeaderTitle">Colour Bold Heading:</div>
                                        <asp:Label ID="Label3" runat="server" CssClass="priceLabel" 
                                            Text='<%# String.Format("{0:C} ", Eval("LineAdColourBoldHeaderAmount")) %>' />
                                    </div>
                                    <div id="colourBackgroundRate" class="priceSection" runat="server">
                                        <div class="priceHeaderTitle">Colour Background:</div>
                                        <asp:Label ID="Label4" runat="server" CssClass="priceLabel" 
                                            Text='<%# String.Format("{0:C} ", Eval("LineAdColourBackgroundAmount")) %>' />
                                    </div>
                                    <div id="ColourBorder" class="priceSection" runat="server">
                                        <div class="priceHeaderTitle">Colour Border:</div>
                                        <asp:Label ID="Label5" runat="server" CssClass="priceLabel" 
                                            Text='<%# String.Format("{0:C} ", Eval("LineAdColourBorderAmount")) %>' />
                                    </div>
                                    <%--<asp:Panel ID="pnlExtraCharge" runat="server">
                                        <div class="specialOfferOtherInfo">
                                            Casual Rate:
                                        </div>
                                        <asp:Label ID="lblCasualRate" runat="server" CssClass="specialOfferOtherInfoBold" />
                                    </asp:Panel>
                                    <br />
                                    
                                    <div class="specialOfferOtherInfo">
                                        Picture Price:</div>
                                    <asp:Label ID="lblPhotoCharge" runat="server" CssClass="specialOfferOtherInfoBold"
                                        Text='<%# String.Format("{0:C}", Eval("PhotoCharge")) %>' />
                                    <br />
                                    <div class="specialOfferOtherInfo">
                                        Bold Heading:</div>
                                    <asp:Label ID="lblBoldHeading" runat="server" CssClass="specialOfferOtherInfoBold"
                                        Text='<%# String.Format("{0:C}", Eval("BoldHeading")) %>'></asp:Label>
                                    <br />
                                    <asp:Panel ID="pnlMaxCharge" runat="server">
                                        <div class="specialOfferOtherInfo">
                                            Maximum Charge:</div>
                                        <asp:Label ID="lblMaxCharge" runat="server" CssClass="specialOfferOtherInfoBold"
                                            Text='<%# String.Format("This rate is capped at: {0:C}", Eval("MaxCharge")) %>' />
                                    </asp:Panel>--%>
                                </asp:Panel>
                                
                                <div class="specialButton">
                                    <asp:LinkButton ID="lnkBook" runat="server" Text="Start Booking!" CommandName="Book" CommandArgument='<%# Eval("AdTypeId") %>' />
                                </div>
                                
                            </ItemTemplate>
                            
                        </asp:DataList>
                    </div>
                </asp:Panel>
                <div id="bookAdMainContent">
                    &nbsp;
                </div>
            </div>
            <div id="mainFooterInfo">
                <em>RETURN TO:</em> <a href="#0">TOP</a> |
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">HOME</asp:HyperLink>
            </div>
        </div>
    </div>
</asp:Content>
