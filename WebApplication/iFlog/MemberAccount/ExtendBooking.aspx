<%@ Page Title="Extend Booking" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MemberDetails.master" CodeBehind="ExtendBooking.aspx.vb" Inherits="BetterclassifiedsWeb.ExtendBooking" %>

<%@ Register Src="~/MemberAccount/MemberHeading.ascx" TagName="MemberHeading" TagPrefix="ucx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="memberHeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="memberContentMain" runat="server">
    <ucx:MemberHeading ID="ucxHeading" runat="server" HeadingText="Booking Extension" />
    
    <%--Todo - Error List--%>

    <%--Number of edition selection--%>
    <paramountIt:FormDropDownList runat="server" ID="ddlEditions"
        Text="Insertions"
        HelpText="Number of weeks to extend the booking" />
    
    <%--List of editions to be booked--%>
    <asp:Repeater runat="server" ID="rptEditions">
        <HeaderTemplate>
            Publication Editions
        </HeaderTemplate>
        <ItemTemplate>
            <div>
                <asp:Label runat="server" ID="lblPublicationName" 
                    Font-Bold="True"
                    Text='<%# Eval("PublicationName") %>'></asp:Label>
                
                <asp:GridView id="grdEditions" runat="server" DataSource='<%# Eval("Editions") %>'
                        GridLines="None" ShowHeader="false"
                        AlternatingRowStyle-BackColor="Beige" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField ShowHeader="False" DataField="EditionDate" DataFormatString="{0:dd-MMM-yyyy}" />
                    </Columns>
                </asp:GridView>
            </div>
        </ItemTemplate>
        <FooterTemplate>
            
        </FooterTemplate>
    </asp:Repeater>
    
    <div>
        Online Schedule
        End Date: <asp:Label runat="server" ID="lblOnlineEndDate"></asp:Label>
    </div>
    
    <div>
        Price per edition
        <asp:Label runat="server" ID="lblPricePerEdition"></asp:Label>
    </div>
    
    <div>
        Total Price
        <asp:Label runat="server" ID="lblTotalPrice"></asp:Label>
    </div>
    
    <div id="divPayment" runat="server">
        <asp:CheckBox ID="chkConditions" runat="server" Text="I have read and agreed to the <a href='../Terms.aspx' target='blank'>terms and conditions</a>." />
    </div>

    <div>
        <asp:Button runat="server" ID="btnCancel" Text="Cancel"/>
        <asp:Button runat="server" ID="btnSubmit" Text="Submit"/>
    </div>

</asp:Content>
