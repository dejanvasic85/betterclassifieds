﻿<%@ Page Title="My Ads Coming Soon" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MemberDetails.master" CodeBehind="UpComing.aspx.vb" Inherits="BetterclassifiedsWeb.UpComing" %>

<%@ Register Src="~/MemberAccount/MemberHeading.ascx" TagName="MemberHeading" TagPrefix="ucx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="memberHeadContent" runat="server">
    <script type="text/javascript">
        function openRadWindow(adID) 
        {
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
    
    <ucx:MemberHeading ID="ucxHeading" runat="server" HeadingText="Up Coming Ads" />
    
    <div id="mainContentMyAccount">
    
        <div class="UserListPanel">
            <h4 class="UserListHeading">ONLINE CLASSIFIEDS</h4>
            <asp:GridView ID="grdOnline" runat="server" Width="740" AutoGenerateColumns="false" 
                            EmptyDataText="You have no up coming online ads." CellPadding="0"
                            GridLines="Horizontal" AllowSorting="false" EnableViewState="false">
                <HeaderStyle CssClass="myAccountTableItemHead" />
                <RowStyle Height="28" />
                <Columns>
                    <asp:TemplateField HeaderText="Ad ID" ItemStyle-CssClass="myAccountTableItemBody">
                        <ItemTemplate>
                            <a href="#"
                                onclick="openRadWindow('<%#DataBinder.Eval(Container.DataItem,"AdDesignId")%>'); return false;">
                            <asp:Literal ID="lbliFlogId" runat="server" 
                                Text='<%#DataBinder.Eval(Container.DataItem,"AdDesignId")%>' /></a>
                            
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:BoundField HeaderText="Ref No" DataField="BookReference" ItemStyle-CssClass="myAccountTableItemBody" />
                    <asp:BoundField HeaderText="Title" DataField="Title" ItemStyle-CssClass="myAccountTableItemBody" />
                    
                    <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="myAccountTableItemBody">
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkStatus" runat="server" Text='<% Eval("Status") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:BoundField HeaderText="Category" DataField="Category" ItemStyle-CssClass="myAccountTableItemBody" />
                    <asp:BoundField HeaderText="Commences" DataField="StartDate" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-CssClass="myAccountTableItemBody" />
                    
                    <asp:TemplateField HeaderText="Edit" ItemStyle-CssClass="myAccountTableItemBody">
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkEdit" runat="server" Text="Edit" 
                                NavigateUrl='<%# String.Format("{0}?dsId={1}&ref={2}&bkId={3}&list=scheduled", "~/MemberAccount/EditOnlineAd.aspx", Eval("AdDesignId"), Server.UrlEncode(Eval("BookReference")), Eval("AdBookingId")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                
            </asp:GridView>
            
        </div>
        
        <div style="margin-top: 30px;" />
        
        <div class="UserListPanel">
            <h4 class="UserListHeading">PRINT ADVERTISEMENTS</h4>
            <asp:GridView ID="grdPrintAds" runat="server" Width="740" AutoGenerateColumns="false" 
                            EmptyDataText="You have no up coming Print Ads." CellPadding="0"
                            GridLines="Horizontal" AllowSorting="false" EnableViewState="true">
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
                    <asp:BoundField HeaderText="Commences" DataField="StartDate" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-CssClass="myAccountTableItemBody" />
                    <asp:TemplateField HeaderText="Edit" ItemStyle-CssClass="myAccountTableItemBody">
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkEdit" runat="server" Text="Edit" NavigateUrl='<%# String.Format("{0}?dsId={1}&bkId={2}&list=scheduled","~/MemberAccount/EditLineAd.aspx", Eval("AdDesignId"), Eval("AdBookingId")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    
    <telerik:RadWindowManager ID="RadWindowManager1" VisibleOnPageLoad="false" Behaviors="Close"
        runat="server" Width="650px" Height="500px" Modal="true" VisibleStatusbar="false" >
    </telerik:RadWindowManager>
</asp:Content>
