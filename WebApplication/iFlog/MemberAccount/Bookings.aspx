<%@ Page Title="My Bookings" Language="vb" AutoEventWireup="false" ClientIDMode="Predictable" CodeBehind="Bookings.aspx.vb" Inherits="BetterclassifiedsWeb.Bookings" MasterPageFile="~/Master/MemberDetails.master" %>

<%@ Register Src="~/MemberAccount/MemberHeading.ascx" TagName="MemberHeading" TagPrefix="ucx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="memberContentMain" runat="server">

    <%--Heading--%>
    <ucx:MemberHeading ID="ucxHeading" runat="server" HeadingText="My Bookings" />

    <%--User Alert--%>
    <div class="accountRow">
        <asp:Panel runat="server" ID="pnlBookingsAboutToExpireAlert" Visible="False" CssClass="alert alert-warning">
            <strong>Note:</strong> There are bookings that are about to expire. Click extend now!
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlBookingCancelledAlert" Visible="False" CssClass="alert alert-success">
            The requested Ad has been cancelled successfully.
        </asp:Panel>

        <asp:Panel runat="server" ID="pnlExtensionComplete" CssClass="alert alert-success" Visible="False">
            Booking extension has been processed successfully.
        </asp:Panel>
    </div>


    <%--Menu--%>
    <div class="accountRow" id="menu" runat="server">
        <ul class="nav nav-pills">
            <li>
                <asp:LinkButton runat="server" ID="lnkViewAll" Text="All"></asp:LinkButton></li>
            <li class="active">
                <asp:LinkButton runat="server" ID="lnkViewCurrent" Text="Current" CssClass="active"></asp:LinkButton></li>
            <li>
                <asp:LinkButton runat="server" ID="lnkViewScheduled" Text="Coming Soon"></asp:LinkButton></li>
            <li>
                <asp:LinkButton runat="server" ID="lnkViewExpired" Text="Expired"></asp:LinkButton></li>
        </ul>
    </div>


    <%--Bookings--%>
    <div class="accountRow">
        <asp:ListView runat="server" ID="lstBookings" ClientIDMode="AutoID">
            <LayoutTemplate>
                <table class="table" runat="server" id="tblBookings">
                    <tbody>
                        <tr runat="server" id="itemPlaceholder"></tr>
                    </tbody>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr runat="server" id="row">
                    <td style="width: 20%">
                        <asp:Image runat="server" ID="adImage" CssClass="thumbnail" />
                    </td>
                    <td style="width: 80%">
                        <div class="adLabel">
                            <label>Ad ID</label>
                            <asp:Literal runat="server" ID="lblAdBookingId" Text='<%#Eval("AdBookingId") %>'></asp:Literal>
                        </div>
                        <div class="adLabel">
                            <label>Title</label>
                            <asp:Literal runat="server" ID="lblTitle" Text='<%#Eval("AdTitle")%>'></asp:Literal>
                        </div>
                        <div class="adLabel">
                            <label>Category</label>
                            <asp:Literal runat="server" ID="lblCategory" Text='<%# Eval("CategoryName")%>'></asp:Literal>
                        </div>
                        <div class="adLabel">
                            <label>Starting</label>
                            <asp:Literal runat="server" ID="Literal2" Text='<%# string.Format("{0:dd-MMM-yyyy}", Eval("StartDate"))%>'></asp:Literal>
                        </div>
                        <div class="adLabel">
                            <label>Ending</label>
                            <asp:Literal runat="server" ID="Literal1" Text='<%# string.Format("{0:dd-MMM-yyyy}", Eval("EndDate"))%>'></asp:Literal>
                        </div>
                        <div class="btn-group pull-right" style="padding-top: 10px;">
                            <asp:HyperLink runat="server" ID="lnkExtend" Text="Extend" CssClass="btn btn-default" />
                            <asp:HyperLink runat="server" ID="lnkCancel" Text="Cancel" CssClass="btn btn-default cancelBooking" />
                            <asp:HyperLink runat="server" ID="lnkViewInvoice" Text="Invoice" CssClass="btn btn-default" Target="Blank" />
                            <asp:LinkButton runat="server" ID="lnkBookAgain" Text="Book Again" CssClass="btn btn-default" Visible="False" CommandArgument='<%# Eval("AdBookingId")%>' CommandName="BookAgain"/>
                            <asp:HyperLink runat="server" ID="lnkEditOnlineAd" Text="Edit Online Ad" CssClass="btn btn-default" />
                            <asp:HyperLink runat="server" ID="lnkEditLineAd" Text="Edit Print Ad" CssClass="btn btn-default" />
                        </div>
                        <div class="alert alert-cancelConfirmation pull-right">
                            <label>
                                This will remove your entire booking.
                                Are you sure you want to continue?
                            </label>

                            <asp:LinkButton runat="server" ID="lnkConfirmCancellation" CommandArgument='<%# Eval("AdBookingId") %>'
                                CommandName="CancelBooking" CssClass="btn btn-default" Text="Yes"></asp:LinkButton>
                            <a class="btn btn-default dismiss">No</a>
                        </div>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="memberPageScripts">
    <script type="text/javascript">
        $().ready(function () {

            $('.alert-cancelConfirmation').closest('tr').find('.alert').hide();

            $('.cancelBooking').on('click', function () {
                $(this).closest('tr').find('.alert-cancelConfirmation').slideToggle();
            });

            $('.dismiss').on('click', function () {
                $(this).parent().slideToggle();
            });

            $('.nav-pills').find('a').each(function () {
                if ($(this).hasClass('active')) {
                    $(this).closest('li').attr('class', 'active');
                    
                } else {
                    $(this).closest('li').removeClass('active');
                }
            });
        });
    </script>
</asp:Content>
