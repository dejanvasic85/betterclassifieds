<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="AdBookings.aspx.vb" Inherits="BetterclassifiedAdmin.AdBookings" %>
<%@ Register Src="~/classified/UserControls/BookingNavigation.ascx" TagName="BookingNavigation" TagPrefix="ucx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function openDesignRadWindow(adID) {
            var oWnd = radopen("ModalDialog/Edit_AdDesigns.aspx?adBookingId=" + adID, "RadWindow1");
            oWnd.center();
        }
        function openEditionRadWindow(adID) {
            var oWnd = radopen("ModalDialog/Edit_BookEntries.aspx?adBookingId=" + adID, "RadWindow1");
            oWnd.center();
        }
        function openAdBookingRadWindow(adID) {
            var oWnd = radopen("ModalDialog/Edit_Adbooking.aspx?adBookingId=" + adID, "RadWindow1");
            oWnd.center();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyTitle" runat="server">
    Manage Classified Bookings
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphUserNavigation" runat="server">
    <ucx:BookingNavigation ID="ucxBookingNavigation" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContentBody" runat="server">
    
    <h4>Search Ad Bookings</h4>
    <table cellpadding="4">
        <tr valign="top">
            <td>
                <table cellpadding="2">
                    <tr>
                        <td>Ad ID:</td>
                        <td><asp:TextBox ID="txtAdId" runat="server" AutoCompleteType="Disabled" /></td>
                    </tr>
                    <tr>
                        <td>Booking Ref:</td>
                        <td><asp:TextBox ID="txtBookReference" runat="server" AutoCompleteType="Disabled" /></td>
                    </tr>
                    <tr>
                        <td>Username:</td>
                        <td><asp:TextBox ID="txtUsername" runat="server" AutoCompleteType="Disabled" /></td>
                    </tr>
                </table>
            </td>
            <td>
            <table cellpadding="2">
                <tr>
                    <td>Booking Date:</td>
                    <td><telerik:RadDatePicker ID="dtmFrom" runat="server"></telerik:RadDatePicker></td>
                </tr>
                <tr>
                    <td></td>
                    <td><telerik:RadDatePicker ID="dtmTo" runat="server"></telerik:RadDatePicker></td>
                </tr>
                <tr>
                    <td>Booking Status:</td>
                    <td><asp:DropDownList ID="ddlStatus" runat="server">
                            <asp:ListItem Text="Any" Value="0" />
                            <asp:ListItem Text="Booked" Value="1" />
                            <asp:ListItem Text="Cancelled" Value="3" />
                            <asp:ListItem Text="Unpaid" Value="4" />
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>Edition Date:</td>
                    <td><telerik:RadDatePicker ID="dtmEditionFrom" runat="server" /></td>
                </tr>
                <tr>
                    <td></td>
                    <td><telerik:RadDatePicker ID="dtmEditionTo" runat="server" /></td>
                </tr>
            </table>
        </td>
            <td>
            <table cellpadding="2">
                <tr>
                    <td>Publication:</td>
                    <td><asp:DropDownList ID="ddlPublication" runat="server" DataTextField="Title" DataValueField="PublicationId" /></td>
                </tr>
                <tr>
                    <td valign="top">Category:</td>
                    <td><asp:UpdatePanel ID="pnlUpdateCategories" runat="server">
                            <ContentTemplate>
                                <div style="padding: 2px"><asp:DropDownList ID="ddlMainCategories" runat="server" DataTextField="Title" DataValueField="MainCategoryId" AutoPostBack="true" Width="200px" /></div>
                                <div style="padding: 2px"><asp:DropDownList ID="ddlSubCategories" runat="server" DataTextField="Title" DataValueField="MainCategoryId" Width="200px" /></div>
                            </ContentTemplate>
                        </asp:UpdatePanel></td>
                </tr>
                <tr>
                    <td>Keyword:</td>
                    <td><asp:TextBox ID="txtKeyword" runat="server" Width="200px"  AutoCompleteType="Disabled"  /></td>
                </tr>
                <tr>
                    <td colspan="4" align="right"><asp:Button ID="btnSearch" runat="server" Text="Search" AutoCompleteType="Disabled" />
                    &nbsp; 
                    <asp:Button ID="btnClear" runat="server" Text="Clear Filters" /></td>
                </tr>
            </table>
        </td>
        </tr>
    </table>
    <br />

    <telerik:RadGrid ID="grdBookingResults" runat="server" 
            DataSourceID="bookingsDataSource"
            Width="97%" 
            AllowSorting="True" 
            AllowPaging="True" 
            AutoGenerateColumns="False"
            PageSize="15"
            Gridlines="None">
        <MasterTableView Width="100%">
        <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="true" />
        <HeaderStyle HorizontalAlign="Left" Wrap="true" Height="35" />
        <AlternatingItemStyle VerticalAlign="Top" />
        <ItemStyle VerticalAlign="Top" />
        <Columns>
            <telerik:GridTemplateColumn HeaderText="Ad ID">
                <ItemTemplate>
                <a href="#"
                        onclick="openAdBookingRadWindow('<%#DataBinder.Eval(Container.DataItem,"AdBookingId")%>'); return false;">
                    <asp:Label ID="lblReference" runat="server" Text='<%# Eval("AdBookingId")%>' ToolTip="Edit Ad Booking Details" /></a>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn DataField="BookReference" HeaderText="Reference" />
            <telerik:GridBoundColumn DataField="ParentCategory" HeaderText="Category" />
            <telerik:GridBoundColumn DataField="SubCategory" HeaderText="Sub Category" />
            <telerik:GridBoundColumn DataField="UserId" HeaderText="UserId" />
            <telerik:GridBoundColumn DataField="BookingDate" HeaderText="Date Booked" DataFormatString="{0:dd-MMM-yyyy}" />
            <telerik:GridBoundColumn DataField="BookingType" HeaderText="Type" />
            <telerik:GridTemplateColumn HeaderText="Status">
                <ItemTemplate>
                    <asp:Literal runat="server" ID="lblBookingStatus"></asp:Literal>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="Editions">
                <ItemTemplate>
                <a href="#"
                        onclick="openEditionRadWindow('<%#DataBinder.Eval(Container.DataItem,"AdBookingId")%>'); return false;">
                    <asp:Label ID="lblEditionText" runat="server" Text="View" /></a>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="Designs">
                <ItemTemplate>
                <a href="#"
                        onclick="openDesignRadWindow('<%#DataBinder.Eval(Container.DataItem,"AdBookingId")%>'); return false;">
                    <asp:Label ID="lblDesignText" runat="server" Text="Edit" /></a>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn DataField="TotalPrice" HeaderText="Price (inc gst)" DataFormatString="{0:C}" />
        </Columns>
        </MasterTableView>
    </telerik:RadGrid>
  
    <asp:ObjectDataSource ID="bookingsDataSource" runat="server" 
        SelectMethod="SearchAdBookings" 
        SelectCountMethod="SearchAdBookingsCount"
        TypeName="BetterClassified.UIController.AdBookingController" 
        EnablePaging="true"/>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" 
        Width="800px" Height="600px" 
        VisibleOnPageLoad="false" DestroyOnClose="true" 
        ReloadOnShow="true" Modal="true" >
    </telerik:RadWindowManager>

</asp:Content>
