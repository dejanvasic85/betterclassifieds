<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Default.Master" CodeBehind="Default.aspx.vb" Inherits="BetterclassifiedsWeb._Default2" Title="Booking Complete" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="clearFloat">
        <div id="contentBodyAccounts">
            <div id="mainBookAd">
                <div id="mainHeaderBookAd" style="height: 90px;">
                    <h3>
                        <asp:Panel ID="pnlHeader" runat="server">
                            <asp:Label ID="lblHeader" runat="server" Text="DONE!" />
                        </asp:Panel></h3>
                    <h5><asp:Label ID="lblBookingSuccess" runat="server" /></h5>
                </div>
                <div id="bookAdMainContent">
                    <p>Where would you like to go from here?</p><br />
                    <h3><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/" Text="Take me to home page!" /></h3><br />
                    <h3><asp:HyperLink ID="lnkBookAd" runat="server" NavigateUrl="~/Booking/Step1.aspx" Text="Book another Ad" /></h3><br />
                    <h3><asp:HyperLink ID="lnkMyAccountDetails" runat="server" NavigateUrl="~/MemberAccount/MemberDetails.aspx" Text="Manage my account and Bookings" /></h3><br />
                    <h3><asp:HyperLink ID="lnkSpecials" runat="server" NavigateUrl="~/Rates.aspx" Text="Find a Special Rate" /></h3>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
