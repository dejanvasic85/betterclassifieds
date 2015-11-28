<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Edit_AdBooking.aspx.vb" Inherits="BetterclassifiedAdmin.Edit_AdBooking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Category Details</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div style="padding: 20px">
        <%-- ajax script manager --%>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
        <%-- ajax update panel start --%>
        <asp:UpdatePanel ID="pnlUpdateGeneralDetails" runat="server">
            <ContentTemplate>
                
                <ajax:TabContainer ID="tcntUserInfo" runat="server" ActiveTabIndex="0" 
                    Width="100%" Font-Size="10px">
                    
                    <ajax:TabPanel ID="TabPanel2" runat="server" HeaderText="General Info">
                        <ContentTemplate>
                        <div style="font-size: 11px;">
                            <br />
                            
                            <asp:DetailsView ID="dtlAdBooking" runat="server" AutoGenerateRows="False"
                                style="width: 445px" HeaderText="Ad Booking Details"
                                DefaultMode="Edit" DataKeyNames="AdBookingId" DataSourceID="linqSource">
                                <Fields>
                                    <asp:BoundField DataField="AdBookingId" HeaderText="Booking ID" ReadOnly="true" />
                                    <asp:BoundField DataField="BookReference" HeaderText="Reference" ReadOnly="true" />
                                    <asp:BoundField DataField="BookingDate" HeaderText="Date Booked" ReadOnly="true" DataFormatString="{0:dd-MMM-yyyy}" />
                                    <asp:BoundField DataField="BookingType" HeaderText="Type" ReadOnly="true" />
                                    <asp:BoundField DataField="Insertions" HeaderText="Insertions" ReadOnly="true" />
                                    <asp:BoundField DataField="TotalPrice" HeaderText="Price (inc gst)" ReadOnly="true" DataFormatString="{0:C}" />
                                    <asp:TemplateField HeaderText="User ID">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtUserId" runat="server" DataField="UserId" Text='<%# Eval("UserId") %>'  />
                                            <asp:ImageButton ID="chkUser" runat="server" ToolTip="Check If User Exists"
                                                             OnClick="CheckUser" ImageUrl="~/images/checknames.gif" />
                                            <asp:Label ID="lblConfirm" runat="server" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Booking Status">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlStatus" runat="server" >
                                                <asp:ListItem Text="Booked" Value="1" />
                                                <asp:ListItem Text="Cancelled" Value="3" />
                                                <asp:ListItem Text="Unpaid" Value="4" />
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Fields>
                            </asp:DetailsView>
                            <table width="90%" class="detailsviewMain" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="2" class="detailsviewheaderBG">
                                        Categories
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 170px;">
                                        <asp:Label ID="Label2" runat="server" Text="Parent Category"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlParentCategory" runat="server" DataTextField="Title" DataValueField="MainCategoryId" AutoPostBack="true" Width="200px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 170px;">
                                        <asp:Label ID="Label1" runat="server" Text="Sub Category"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSubCategory" runat="server" DataTextField="Title" DataValueField="MainCategoryId" Width="200px" />
                                    </td>
                                </tr>

                            </table>
                            <asp:LinqDataSource ID="linqSource" runat="server" EnableUpdate="true" EnableObjectTracking="false"
                                 ContextTypeName="BetterclassifiedsCore.DataModel.BetterclassifiedsDataContext" TableName="AdBookings" />
                        </div>
                        </ContentTemplate>
                    </ajax:TabPanel>
                </ajax:TabContainer>
                
                <div class="controlDiv">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" />
                </div>
                
                <br />
                
                <div>
                    <asp:Label ID="lblMessage" runat="server" Text="" />
                </div>
                
            </ContentTemplate>
        </asp:UpdatePanel>
        
        <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="pnlUpdateGeneralDetails">
            <ProgressTemplate>
                <table>
                    <tr>
                        <td align="center">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/blue/Images/progress_circle.gif" Height="20" Width="20" /></td>
                        <td>
                            <asp:Label ID="lblProgress" runat="server" Text="Processing..." /></td>
                    </tr>
                </table>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    </div>
    </form>
</body>
</html>
