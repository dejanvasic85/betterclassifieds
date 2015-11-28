<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MemberDetails.master"
    CodeBehind="Messages.aspx.vb" Inherits="BetterclassifiedsWeb.Messages" Title="Messages" %>

<%@ Register Src="~/MemberAccount/MemberHeading.ascx" TagName="MemberHeading" TagPrefix="ucx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="memberHeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="memberContentMain" runat="server">
    <ucx:MemberHeading ID="ucxHeading" runat="server" HeadingText="My Messages" />
    <div style="clear:both;"></div>
    <telerik:RadGrid ID="messageGrid" runat="server" AutoGenerateColumns="false">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn HeaderText="Date" DataField="CreatedDate" DataType="System.DateTime"
                    DataFormatString="{0:dd-MMM-yyyy}">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FullName" HeaderText="Full Name">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Email" HeaderText="Email Address">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Phone" HeaderText="Phone Number" />
                <telerik:GridBoundColumn DataField="EnquiryText" HeaderText="Message" />
            </Columns>
            <NoRecordsTemplate>
                <p>
                    You have no messages
                </p>
            </NoRecordsTemplate>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
