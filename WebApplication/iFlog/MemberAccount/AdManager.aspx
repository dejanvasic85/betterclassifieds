<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MemberDetails.master" 
    Inherits="BetterclassifiedsWeb.AdManager" CodeBehind="AdManager.aspx.vb" %>

<%@ Register Src="~/MemberAccount/MemberHeading.ascx" TagName="MemberHeading" TagPrefix="ucx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="memberHeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="memberContentMain" runat="server">
    
    <ucx:MemberHeading ID="ucxHeading" runat="server" HeadingText="Manage Ad Bookings" />

    <telerik:RadGrid runat="server" ID="grdBookings" OnNeedDataSource="grdBookings_NeedDataSource">
        <MasterTableView>            
            <NoRecordsTemplate>
                There are no items to display.
            </NoRecordsTemplate>
            <Columns>
                
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
