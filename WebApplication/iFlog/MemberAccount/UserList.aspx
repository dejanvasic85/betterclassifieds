<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MemberDetails.master"
    CodeBehind="UserList.aspx.vb" Inherits="BetterclassifiedsWeb.UserList" %>

<%@ Register Src="~/Controls/Search/RecentOnlineAdList.ascx" TagName="RecentlyAdded" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/LineAdPreview.ascx" TagName="LineAdView" TagPrefix="ucx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="memberContentMain" runat="server">
   
    <div id="mainHeaderMyAccount">
        <asp:Image ID="imgAccHeader" runat="server" ImageUrl="~/Resources/Images/my_account_header.gif" AlternateText="My Account" />
        <h2>
            Welcome, 
            <asp:LoginName ID="LoginName1" runat="server"  />
        </h2>
            <h3>
                <asp:Label ID="lblHeader" runat="server" Text=""></asp:Label></h3>
    </div>
    
    <div id="mainContentMyAccount">
    
        <div style="margin-bottom: 5px; margin-top: 5px; margin-left: 20px;">
            <asp:Label ID="lblEditInformation" runat="server" Text="" ForeColor="Green"></asp:Label>
        </div>
        
        <div id="myAccountTableBody">
            <asp:Panel ID="pnlExpiredList" runat="server" Visible="false">
            <%-- Make use of your previous designs to make another booking! We keep a history of 
                    your designs for up to 3 months after their booking expires.--%>
            
                <asp:GridView ID="grdExpiredAds" Width="740" runat="server" AutoGenerateColumns="false" 
                    EmptyDataText="You have no expired designs." CellPadding="0"
                    GridLines="Horizontal" AllowSorting="false" EnableViewState="false" ShowHeader="true">
                    
                    <HeaderStyle CssClass="myAccountTableItemHead" />
                    
                    <Columns>

                        <asp:TemplateField HeaderText="Date Expired" ItemStyle-Height="28" ItemStyle-CssClass="myAccountTableItemBody">
                            <ItemTemplate>
                                <asp:Label ID="lblEndDate" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("EndDate")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Title" ItemStyle-CssClass="myAccountTableItemBody">
                            <ItemTemplate>
                                <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Type" ItemStyle-CssClass="myAccountTableItemBody">
                            <ItemTemplate>
                                <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("AdType") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Select" ItemStyle-CssClass="myAccountTableItemBody">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkReschedule" runat="server" Text="Book Again" CausesValidation="false" 
                                    PostBackUrl='<%# String.Format("~/Booking/Step1.aspx?action={0}&adDesignId={1}&adTypeId={2}", "reschedule", Eval("AdDesignId"), Eval("AdTypeId")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                    </Columns>
                    
                </asp:GridView>
           
            </asp:Panel>  
            
            <asp:UpdatePanel ID="pnlUpdateCurrent" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlSchedule" runat="server" Visible="false">
                        
                            <%--<p>You may edit the design at any time before it goes live. </p>
                            <p>Online Ads are approved by an administrator and you will be notified once it has been approved or cancelled.</p>
                            --%>
                            
                        <asp:GridView ID="grdScheduledList" Width="740" runat="server" AutoGenerateColumns="False"  
                            EmptyDataText="You have no scheduled bookings."
                            GridLines="Horizontal" AllowSorting="false" Visible="false" CellPadding="0">
                                
                            <HeaderStyle CssClass="myAccountTableItemHead" />
                            
                            <Columns>
                                
                                <asp:BoundField DataField="BookReference" HeaderText="iFlog ID" ReadOnly="true"
                                    SortExpression="BookReference" ItemStyle-CssClass="myAccountTableItemBody" />
                                    
                                <asp:BoundField DataField="StartDate" HeaderText="Start Date" ReadOnly="true" SortExpression="StartDate"
                                    DataFormatString="{0:dd/M/yyyy}" ItemStyle-CssClass="myAccountTableItemBody" />
                                    
                                <asp:BoundField DataField="EndDate" HeaderText="End Date" ReadOnly="true" SortExpression="EndDate"
                                    DataFormatString="{0:dd/M/yyyy}" ItemStyle-CssClass="myAccountTableItemBody" />
                                    
                                <asp:BoundField DataField="MainCategory" HeaderText="Main Category" ReadOnly="true"
                                    SortExpression="MainCategory" ItemStyle-CssClass="myAccountTableItemBody" />
                                    
                                <asp:BoundField DataField="Title" HeaderText="Ad Title" ReadOnly="true" 
                                    SortExpression="Title" ItemStyle-CssClass="myAccountTableItemBody" ItemStyle-Width="200" />
                                
                                <%--Edit Designs--%>
                                <asp:TemplateField HeaderText="Edit" ItemStyle-CssClass="myAccountTableItemBody" ItemStyle-Height="28">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit Ads" PostBackUrl='<%# "~/MemberAccount/EditDesigns.aspx?bk=" + String.Format("{0}", Eval("AdBookingId")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <%--Cancel Button--%>
                                <asp:TemplateField HeaderText="Cancel" ItemStyle-CssClass="myAccountTableItemBody">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkTest" runat="server" Text="Cancel" CommandName="Select" />
                                        <asp:HiddenField ID="hdnBookingId" runat="server" Value='<%# Eval("AdBookingId") %>' />
                                        <asp:Panel ID="pnlProject" runat="server" Style="display: none" CssClass="modalPopup">
                                            
                                            <asp:Label ID="Label1" runat="server" Text="Please ensure you have read the <a href='../Terms.aspx' target='blank'>terms and conditions</a> before cancelling an ad booking. <br><br>Are you sure you want to continue?"></asp:Label>
                                            
                                            <div align="center">
                                                <asp:Button ID="btnYes" runat="server" Text="Yes" OnClick="CancelBooking" />
                                                <asp:Button ID="btnNo" runat="server" Text="Cancel" />
                                            </div>
                                        </asp:Panel>
                                        <ajax:ModalPopupExtender ID="extProject" runat="server" TargetControlID="btnTrigger"
                                            PopupControlID="pnlProject" DropShadow="true" BackgroundCssClass="modalBackground"
                                            CancelControlID="btnNo" />
                                        <asp:Button ID="btnTrigger" runat="server" Style="display: none" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>
                            
                        </asp:GridView>
                        
                    </asp:Panel>
            
                    <asp:Panel ID="pnlCurrentList" runat="server" Visible="false">
                                                
                        <%--<p>Please note: You may only be able to edit and re-publish an 
                        online ad if it has been cancelled by our staff.</p>--%>
                        
                        <h4 style="margin: 5px;">ONLINE CLASSIFIEDS</h4>
                        
                        <asp:GridView ID="grdCurrentList" Width="740" runat="server" AutoGenerateColumns="false" 
                            EmptyDataText="You have no running Online Ads."
                            GridLines="Horizontal" AllowSorting="false" CellPadding="1">
                            
                            <HeaderStyle CssClass="myAccountTableItemHead" />
                            
                            <Columns>
                                
                                <asp:TemplateField HeaderText="iFlog ID"
                                    ItemStyle-CssClass="myAccountTableItemBody" ItemStyle-Height="28" >
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkTitle" runat="server" Target="_blank"
                                            ToolTip="Click her to view the ad"
                                            Text='<%# Eval("BookReference") %>'
                                            NavigateUrl='<%# String.Format("{0}{1}", "~/OnlineAds/Adview.aspx?type=ref&preview=true&id=", Eval("BookReference")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    
                                <asp:BoundField DataField="StartDate" HeaderText="Start Date" ReadOnly="true" 
                                    DataFormatString="{0:dd/M/yyyy}" ItemStyle-CssClass="myAccountTableItemBody" />
                                    
                                <asp:BoundField DataField="EndDate" HeaderText="End Date" ReadOnly="true" 
                                    DataFormatString="{0:dd/M/yyyy}" ItemStyle-CssClass="myAccountTableItemBody" />
                                    
                                <asp:BoundField DataField="MainCategory" HeaderText="Main Category" ReadOnly="true"
                                    ItemStyle-CssClass="myAccountTableItemBody" />
                               
                                <asp:BoundField DataField="Title" HeaderText="Ad Title" ReadOnly="true"
                                    ItemStyle-CssClass="myAccountTableItemBody" ItemStyle-Wrap="true" ItemStyle-Width="150"  />
                             
                                <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="myAccountTableItemBody">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkStatus" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <%--Edit Button--%>
                                <asp:TemplateField HeaderText="Edit" ItemStyle-CssClass="myAccountTableItemBody">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkEdit" runat="server" Text="Edit" 
                                            NavigateUrl='<%# String.Format("{0}{1}{2}{3}", "~/MemberAccount/EditOnlineAd.aspx?des=", Eval("AdDesignId"), "&ref=", Server.UrlEncode(Eval("BookReference"))) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <%--Cancel Button--%>
                                <asp:TemplateField HeaderText="Cancel" ItemStyle-CssClass="myAccountTableItemBody">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkTest" runat="server" Text="Cancel" CommandName="Select" />
                                        <asp:HiddenField ID="hdnBookingId" runat="server" Value='<%# Eval("AdBookingId") %>' />
                                        <asp:Panel ID="pnlProject" runat="server" Style="display: none" CssClass="modalPopup">
                                            
                                            <asp:Label ID="Label1" runat="server" Text="Please ensure you have read the <a href='../Terms.aspx' target='blank'>terms and conditions</a> before cancelling an ad booking. <br><br>Are you sure you want to continue?"></asp:Label>
                                            
                                            <div align="center">
                                                <asp:Button ID="btnYes" runat="server" Text="Yes" OnClick="CancelBooking" />
                                                <asp:Button ID="btnNo" runat="server" Text="Cancel" />
                                            </div>
                                        </asp:Panel>
                                        <ajax:ModalPopupExtender ID="extProject" runat="server" TargetControlID="btnTrigger"
                                            PopupControlID="pnlProject" DropShadow="true" BackgroundCssClass="modalBackground"
                                            CancelControlID="btnNo" />
                                        <asp:Button ID="btnTrigger" runat="server" Style="display: none" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns> 
                               
                        </asp:GridView>
                         
                        <br />
                        
                        <h4 style="margin: 5px;">PRINT LINE AD CLASSIFIEDS</h4>
                        
                        <asp:GridView ID="grdCurrentLineAds" runat="server" Width="740" 
                            AutoGenerateColumns="false" EmptyDataText="You have no running Print Line Ads."
                            GridLines="Horizontal" AllowSorting="false" CellPadding="4">
                                
                            <HeaderStyle CssClass="myAccountTableItemHead" />
                            
                            <Columns>
                                   
                                <asp:TemplateField HeaderText="iFlog ID"
                                    ItemStyle-CssClass="myAccountTableItemBody" ItemStyle-Height="28">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkView" runat="server" Text='<%# Eval("BookReference") %>' 
                                            CommandName="View"
                                            CommandArgument='<%# Eval("AdDesignId") %>'
                                            ToolTip="Click here to preview the Line Ad" />
                                            
                                        <asp:Panel ID="pnlLineAdView" runat="server" CssClass="modalPopupLinePreview">
                                            
                                            
                                                <ucx:LineAdView ID="ucxLinePreview" runat="server" />
                                                
                                                <div align="center">
                                                    <asp:Button ID="btnClose" runat="server" Text="Close" />
                                                </div>
                                            
                                            
                                        </asp:Panel>
                                        <ajax:ModalPopupExtender ID="extLinePopup" runat="server" TargetControlID="btnTriggerLine"
                                            PopupControlID="pnlLineAdView" DropShadow="true" BackgroundCssClass="modalBackground"
                                            CancelControlID="btnClose" />
                                        <asp:Button ID="btnTriggerLine" runat="server" Style="display: none" />
                                        
                                        
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 
                                <asp:BoundField DataField="StartDate" HeaderText="Start Date" ReadOnly="true" 
                                    DataFormatString="{0:dd/M/yyyy}" ItemStyle-CssClass="myAccountTableItemBody" />
                                    
                                <asp:BoundField DataField="EndDate" HeaderText="End Date" ReadOnly="true" 
                                    DataFormatString="{0:dd/M/yyyy}" ItemStyle-CssClass="myAccountTableItemBody" />
                                    
                                <asp:BoundField DataField="MainCategory" HeaderText="Main Category" ReadOnly="true"
                                    ItemStyle-CssClass="myAccountTableItemBody" />
                                    
                                <asp:BoundField DataField="Title" HeaderText="Ad Title" ReadOnly="true"
                                    ItemStyle-CssClass="myAccountTableItemBody" ItemStyle-Width="150"  />
                                    
                                <%--Cancel Button--%>
                                <asp:TemplateField HeaderText="Cancel" ItemStyle-CssClass="myAccountTableItemBody">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkTest" runat="server" Text="Cancel" CommandName="Select" />
                                        <asp:HiddenField ID="hdnBookingId" runat="server" Value='<%# Eval("AdBookingId") %>' />
                                        <asp:Panel ID="pnlProject" runat="server" Style="display: none" CssClass="modalPopup">
                                            
                                            <asp:Label ID="Label1" runat="server" Text="Please ensure you have read the <a href='../Terms.aspx' target='blank'>terms and conditions</a> before cancelling an ad booking. <br><br>Are you sure you want to continue?"></asp:Label>
                                            
                                            <div align="center">
                                                <asp:Button ID="btnYes" runat="server" Text="Yes" OnClick="CancelBooking" />
                                                <asp:Button ID="btnNo" runat="server" Text="Cancel" />
                                            </div>
                                        </asp:Panel>
                                        <ajax:ModalPopupExtender ID="extProject" runat="server" TargetControlID="btnTrigger"
                                            PopupControlID="pnlProject" DropShadow="true" BackgroundCssClass="modalBackground"
                                            CancelControlID="btnNo" />
                                        <asp:Button ID="btnTrigger" runat="server" Style="display: none" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>
                                
                        </asp:GridView>
                        
                    </asp:Panel>
                    
                </ContentTemplate>
            </asp:UpdatePanel>
             
        </div>                
    </div>
    
</asp:Content>
