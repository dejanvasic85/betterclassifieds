<%@ Page Title="My Expired Ads" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MemberDetails.master"
    CodeBehind="Expired.aspx.vb" Inherits="BetterclassifiedsWeb.Expired" %>

<%@ Register Src="~/MemberAccount/MemberHeading.ascx" TagName="MemberHeading" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/LineAdPreview.ascx" TagName="LineAdView" TagPrefix="ucx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="memberHeadContent" runat="server">
    <script type="text/javascript">
        function openRadWindow(adID) {
            var oWnd = radopen("../OnlineAds/Preview.aspx?viewType=db&id=" + adID, "RadWindow1");
            oWnd.center();
        }
        function openLineRadWindow(adID) {
            var oWnd = radopen("../LineAds/PreviewLineAd.aspx?viewType=db&id=" + adID, "RadWindow1");
            oWnd.center();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="memberContentMain" runat="server">
    <ucx:MemberHeading ID="ucxHeading" runat="server" HeadingText="Expired Ads" />
    <div id="mainContentMyAccount">
        <div id="bookAdMainContent">
            <p>
                Ads are visible for the past
                <asp:Label ID="lblMonths" runat="server" />
                month(s) since they expired.</p>
        </div>
        <br />
        <br />
        <div class="UserListPanel">
            <h4 class="UserListHeading">
                ONLINE CLASSIFIEDS</h4>
            <asp:GridView ID="grdOnline" runat="server" Width="740px" AutoGenerateColumns="False"
                EmptyDataText="You have no expired online ads." CellPadding="0" GridLines="Horizontal">
                <HeaderStyle CssClass="myAccountTableItemHead" />
                <RowStyle Height="28" />
                <Columns>
                    <asp:TemplateField HeaderText="Ad ID" ItemStyle-CssClass="myAccountTableItemBody">
                        <ItemTemplate>
                            <a href="#" onclick="openRadWindow('<%#DataBinder.Eval(Container.DataItem,"AdDesignId")%>'); return false;">
                                <asp:Literal ID="lbliFlogId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AdDesignId")%>' /></a>
                        </ItemTemplate>
                        <ItemStyle CssClass="myAccountTableItemBody"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Ref No" DataField="BookReference" ItemStyle-CssClass="myAccountTableItemBody">
                        <ItemStyle CssClass="myAccountTableItemBody"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Title" DataField="Title" ItemStyle-CssClass="myAccountTableItemBody">
                        <ItemStyle CssClass="myAccountTableItemBody"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Category" DataField="Category" ItemStyle-CssClass="myAccountTableItemBody">
                        <ItemStyle CssClass="myAccountTableItemBody"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Expired" DataField="EndDate" DataFormatString="{0:dd-MMM-yyyy}"
                        ItemStyle-CssClass="myAccountTableItemBody">
                        <ItemStyle CssClass="myAccountTableItemBody"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Book" ItemStyle-CssClass="myAccountTableItemBody">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkReschedule" runat="server" 
                                Text="Book Now" 
                                CausesValidation="false"
                                CommandArgument='<%# Eval("AdDesignId") %>' 
                                CommandName="bookagain" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div style="margin-top: 30px;" />
        <div class="UserListPanel">
            <h4 class="UserListHeading">
                PRINT ADVERTISEMENTS</h4>
            <asp:GridView ID="grdPrintAds" runat="server" Width="740" AutoGenerateColumns="false"
                EmptyDataText="You have no expired Print Ads." CellPadding="0" GridLines="Horizontal"
                AllowSorting="false" EnableViewState="true">
                <HeaderStyle CssClass="myAccountTableItemHead" />
                <RowStyle Height="28" />
                <Columns>
                    <asp:TemplateField HeaderText="Print ID" ItemStyle-CssClass="myAccountTableItemBody">
                        <ItemTemplate>
                            <a href="#"
                                onclick="openLineRadWindow('<%#DataBinder.Eval(Container.DataItem,"AdDesignId")%>'); return false;">
                            <asp:Literal ID="lbliFlogId" runat="server" 
                                Text='<%#DataBinder.Eval(Container.DataItem,"AdDesignId")%>' /></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Ref No" DataField="BookReference" ItemStyle-CssClass="myAccountTableItemBody" />
                    <asp:BoundField HeaderText="Title" DataField="Title" ItemStyle-CssClass="myAccountTableItemBody" />
                    <asp:BoundField HeaderText="Category" DataField="Category" ItemStyle-CssClass="myAccountTableItemBody" />
                    <asp:BoundField HeaderText="Expired" DataField="EndDate" DataFormatString="{0:dd-MMM-yyyy}"
                        ItemStyle-CssClass="myAccountTableItemBody" />
                    <asp:TemplateField HeaderText="Book" ItemStyle-CssClass="myAccountTableItemBody">
                        <ItemTemplate>
                            <%--<asp:LinkButton ID="lnkReschedule" runat="server" Text="Book Now" 
                                    CausesValidation="false" 
                                    CommandArgument='<%# Eval("AdDesignId") %>' 
                                    CommandName="bookagain"
                                    PostBackUrl='<%# String.Format("~/Booking/Step1.aspx?action={0}&adDesignId={1}&adTypeId={2}", "reschedule", Eval("AdDesignId"), Eval("AdTypeId")) %>' />--%>
                            <asp:LinkButton ID="lnkReschedule" runat="server" 
                                Text="Book Now" 
                                CausesValidation="false"
                                CommandArgument='<%# Eval("AdDesignId") %>' 
                                CommandName="bookagain" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <telerik:RadWindowManager ID="RadWindowManager1" VisibleOnPageLoad="false" runat="server" Behaviors="Close"
        Width="650px" Height="500px" Modal="true" VisibleStatusbar="false" >
    </telerik:RadWindowManager>
</asp:Content>
