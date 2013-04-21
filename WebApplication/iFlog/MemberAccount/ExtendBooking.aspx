<%@ Page Title="Extend Booking" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MemberDetails.master" CodeBehind="ExtendBooking.aspx.vb" Inherits="BetterclassifiedsWeb.ExtendBooking" %>

<%@ Register Src="~/MemberAccount/MemberHeading.ascx" TagName="MemberHeading" TagPrefix="ucx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="memberHeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="memberContentMain" runat="server">
    <ucx:MemberHeading ID="ucxHeading" runat="server" HeadingText="Booking Extension" />

    <telerik:RadAjaxLoadingPanel runat="server" ID="radLoadingPanel" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    
    <telerik:RadAjaxManager runat="server" ID="ajaxManager" DefaultLoadingPanelID="radLoadingPanel">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlEditions">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlForm" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    
    <div class="formcontrol-container" id="errorDetails" runat="server" Visible="False">
        <div class="error">
            <asp:Literal runat="server" ID="lblErrorMessage"></asp:Literal>
        </div> 
    </div>
    
    <asp:Panel runat="server" ID="pnlContent">

        <%--Number of edition selection--%>
        <paramountIt:FormDropDownList runat="server" ID="ddlEditions"
            Text="Insertions"
            HelpText="Number of weeks to extend the booking" />

        <asp:Panel runat="server" ID="pnlForm" DefaultButton="btnSubmit">
    
            <div class="formcontrol-container" runat="server" id="divPublications">
                <label>Publications</label>
                <label class="helptext">Please review the editions below for each publication.</label>

                <div class="control">
                    <%--List of editions to be booked--%>
                    <asp:Repeater runat="server" ID="rptEditions">
                        <ItemTemplate>
                            <div>
                                Publication: <asp:Label runat="server" ID="lblPublicationName" Font-Italic="True" Text='<%# Eval("PublicationName") %>'></asp:Label>

                                <asp:GridView ID="grdEditions" runat="server" DataSource='<%# Eval("Editions") %>'
                                    GridLines="None" ShowHeader="false"
                                    AutoGenerateColumns="False"
                                    Style="font-size: 0.8em; margin-left: 5px;">
                                    <Columns>
                                        <asp:BoundField ShowHeader="False" DataField="EditionDate" DataFormatString="{0:dd-MMM-yyyy}" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ItemTemplate>
                        <SeparatorTemplate>
                            <hr />
                        </SeparatorTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <div class="formcontrol-container" id="divOnlineSchedule" runat="server">
                <label>Online Schedule</label>
                <label class="helptext">The final date for your online ad</label>
                <asp:Label runat="server" ID="lblOnlineEndDate" CssClass="control"></asp:Label>
            </div>

            <div class="formcontrol-container" id="divPricePerEdition" runat="server">
                <label>Price Per Edition</label>
                <asp:Label runat="server" ID="lblPricePerEdition" CssClass="control"></asp:Label>
            </div>

            <div class="formcontrol-container" id="divTotalPrice" runat="server">
                <label>Total Price</label>
                <label class="helptext">Includes all the publication editions</label>
                <asp:Label runat="server" ID="lblTotalPrice" CssClass="control"></asp:Label>
            </div>

            <div class="formcontrol-container" id="divPayment" runat="server">
                <div class="control">
                    <asp:CheckBox ID="chkConditions" runat="server"
                        Text="I have read and agreed to the <a href='../Terms.aspx' target='blank'>terms and conditions</a>." />
                </div>
            </div>

        </asp:Panel>

        <div class="formcontrol-container">
            <asp:Button runat="server" ID="btnCancel" Text="Cancel" />
            <asp:Button runat="server" ID="btnSubmit" Text="Submit" CssClass="pull-right" />
        </div>
    </asp:Panel>

</asp:Content>
