<%@ Page Title="My Classies" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MemberDetails.master"
    CodeBehind="Obsolete_Bookings.aspx.vb" Inherits="BetterclassifiedsWeb.Obsolete_Bookings" %>

<%@ Register Src="~/MemberAccount/MemberHeading.ascx" TagName="MemberHeading" TagPrefix="ucx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="memberHeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="memberContentMain" runat="server">

    <ucx:MemberHeading ID="ucxHeading" runat="server" HeadingText="My Active Bookings" />

    <div id="mainContentMyAccount">
        
        <div id="bookAdMainContent">
            <div class="div-sucess" id="successMessage" runat="server" Visible="False">
                Booking extension has been processed successfully.
            </div>
            <div class="div-warning" id="highlightWarning" runat="server" visible="False" >
                Note: The highlighted bookings are expiring this week. You may start extending your ads by clicking on the calendar icon on the desired booking.
            </div>
        </div>

        <br />
        <br />

        <div class="UserListPanel">
            <asp:GridView ID="grdBookings" runat="server" Width="740" AutoGenerateColumns="false"
                EmptyDataText="You have no active bookings." CellPadding="0"
                GridLines="Horizontal" AllowSorting="false" EnableViewState="true">
                <HeaderStyle CssClass="myAccountTableItemHead" />
                <RowStyle Height="28" />
                <Columns>
                    <asp:BoundField HeaderText="Ref No" DataField="BookReference" ItemStyle-CssClass="myAccountTableItemBody" />
                    <asp:BoundField HeaderText="Start Date" DataField="StartDate" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-CssClass="myAccountTableItemBody" />
                    <asp:BoundField HeaderText="End Date" DataField="EndDate" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-CssClass="myAccountTableItemBody" />
                    <asp:BoundField HeaderText="Category" DataField="Title" ItemStyle-CssClass="myAccountTableItemBody" />
                    <asp:BoundField HeaderText="Price" DataField="TotalPrice" DataFormatString="{0:c}" ItemStyle-CssClass="myAccountTableItemBody" />
                    <asp:BoundField HeaderText="Ads" DataField="NumOfAds" ItemStyle-CssClass="myAccountTableItemBody" />
                    <asp:TemplateField HeaderText="Extend">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:HyperLink runat="server" ID="lnkToExtensions" ToolTip="Extend Booking" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "AdBookingId", "~/MemberAccount/ExtendBooking.aspx?AdBookingId={0}") %>' ImageUrl="~/Resources/Images/date.png" Width="24" Height="24" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton runat="server" CommandName="CancelBooking" ToolTip="Cancel Booking" CommandArgument='<%# Eval("AdBookingId")%>' ImageUrl="~/Resources/Images/Trash.png" OnClientClick="return confirm('Both online and line ads will be cancelled for the selected booking from iFlog. Do you wish to continue?');" Width="24" Height="24" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
