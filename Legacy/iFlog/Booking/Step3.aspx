<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Default.Master"
    CodeBehind="Step3.aspx.vb" Inherits="BetterclassifiedsWeb.Step3"
    Title="New Booking - Design Ad" %>

<%@ Register Src="~/Controls/Booking/DesignLineAd.ascx" TagName="DesignLineAd" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/Booking/DesignOnlineAd.ascx" TagName="DesignOnlineAd" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/Booking/NavigationButtons.ascx" TagName="NavigationButtons" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/ErrorList.ascx" TagName="PageErrors" TagPrefix="ucx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="clearFloat">
        <div id="contentBodyAccounts">
            <div id="mainBookAd">
                <div class="mainBookingHeader">
                    <h3>Design your Print and Online Ads</h3>
                </div>
                <div id="mainContentMyAccount">
                    <%--Page Error List--%>
                    <div id="bookAdMainContent">
                        <ucx:PageErrors ID="ucxPageErrors" runat="server" />
                    </div>

                    <div id="bookAdMainContent">
                        <h1>Upload Images</h1>
                        
                        <div class="help-context-panel">
                            <paramountItCommon:HelpContextControl Position="Bottom" ID="helpContextPanel" ImageUrl="~/Resources/Images/question_button.gif"
                                runat="server" CssClass="upload-help">
                                <ContentTemplate>
                                    <span class="text-wrapper">
                                        <b>Image:</b>
                                        You have the option of selecting
                                        an image to enhance the appeal of your ad to the public. 
                                    </span>
                                </ContentTemplate>
                            </paramountItCommon:HelpContextControl>
                        </div>
                    </div>
                    <div id="bookAdMainContent">
                        <div class="alert alert-info">
                                        Your pictures will be resized to 900 pixel width and 500 pixel height. 
                                        If you prefer no automatic resizing, please upload an image with this 
                                        size ratio. Click 'Upload' to begin.
                                    </div>
                        <div class="wordcount">
                            <asp:LinkButton ID="lnkUploadImages" runat="server" Text="Upload" />
                        </div>
                    </div>
                    <%--Telerik RadWindow for displaying and managing online ad images--%>
                    <telerik:RadWindow ID="radWindowImages" runat="server" NavigateUrl="~/Common/UploadBookingManager.aspx"
                        Title="Upload Images" Width="680px" Height="580px" Behaviors="Close" Modal="true" ReloadOnShow="true" VisibleStatusbar="false" />

                    <ucx:DesignLineAd ID="ucxLineAdDesign" runat="server" Visible="false" ShowWordCount="true" />                

                    <ucx:DesignOnlineAd ID="ucxDesignOnlineAd" runat="server" Visible="false" />
                    <br />
                    <br />

                    <%--Bottom Navigation Buttons--%>
                    <ucx:NavigationButtons ID="ucxNavButtons" runat="server" StepNumber="3" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
