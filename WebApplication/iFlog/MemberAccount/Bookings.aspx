<%@ Page Title="iFlog Bookings" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MemberDetails.master"
    CodeBehind="Bookings.aspx.vb" Inherits="BetterclassifiedsWeb.Bookings" %>

<%@ Register Src="~/MemberAccount/MemberHeading.ascx" TagName="MemberHeading" TagPrefix="ucx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="memberHeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="memberContentMain" runat="server">
        
 <ucx:MemberHeading ID="ucxHeading" runat="server" HeadingText="My Active Bookings" />
    
    <div id="mainContentMyAccount">
    
        <div id="bookAdMainContent">
            <p>Please Note: Cancelling a booking that consists of multiple ads will remove all entries from iFlog.</p>
        </div>
        
        <br /><br />
        
        <div class="UserListPanel">
            
            <asp:UpdatePanel ID="pnlUpdateBookings" runat="server">
                <ContentTemplate>
            
                    <asp:GridView ID="grdBookings" runat="server" Width="740" AutoGenerateColumns="false" 
                                    EmptyDataText="You have no active bookings." CellPadding="0"
                                    GridLines="Horizontal" AllowSorting="false" EnableViewState="true">
                        <HeaderStyle CssClass="myAccountTableItemHead" />
                        <RowStyle Height="28" />
                        <Columns>
                            
                            <%--Check box for selection--%>
                            <asp:TemplateField HeaderText="Select" ItemStyle-CssClass="myAccountTableItemBody">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" Text="" />
                                    <asp:HiddenField ID="hdnBookingId" runat="server" Value='<%# Eval("AdBookingId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:BoundField HeaderText="Ref No" DataField="BookReference" ItemStyle-CssClass="myAccountTableItemBody" />
                            <asp:BoundField HeaderText="Start Date" DataField="StartDate" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-CssClass="myAccountTableItemBody" />
                            <asp:BoundField HeaderText="End Date" DataField="EndDate" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-CssClass="myAccountTableItemBody" />
                            <asp:BoundField HeaderText="Category" DataField="Title" ItemStyle-CssClass="myAccountTableItemBody" />
                            <asp:BoundField HeaderText="Price" DataField="TotalPrice" DataFormatString="{0:c}" ItemStyle-CssClass="myAccountTableItemBody" />
                            <asp:BoundField HeaderText="Ads" DataField="NumOfAds" ItemStyle-CssClass="myAccountTableItemBody" />

                        </Columns>
                    </asp:GridView>
                
                    <asp:Panel ID="pnlButton" runat="server">
                        
                        <%--Modal Popup--%>
                        <asp:Panel ID="pnlProject" runat="server" Style="display: none" CssClass="modalPopup">
                                                    
                        <asp:Label ID="Label1" runat="server" Text="Please ensure you have read the <a href='../Terms.aspx' target='blank'>terms and conditions</a> before cancelling an ad booking. <br><br>Are you sure you want to continue?"></asp:Label>
                                
                            <div align="center">
                                <asp:Button ID="btnYes" runat="server" Text="Yes" OnClick="CancelBooking" />
                                <asp:Button ID="btnNo" runat="server" Text="Cancel" />
                            </div>
                        </asp:Panel>
                        
                        <ajax:ModalPopupExtender ID="extProject" runat="server" TargetControlID="btnCancel"
                            PopupControlID="pnlProject" DropShadow="true" BackgroundCssClass="modalBackground"
                            CancelControlID="btnNo" />
                        <asp:Button ID="btnTrigger" runat="server" Style="display: none" />
                    
                        <div id="myAccountTableButtonsAll" style="width: 200px">
                            <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel Selected Items" />
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            
        </div>
        
    </div>

</asp:Content>
