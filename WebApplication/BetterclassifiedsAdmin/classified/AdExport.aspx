<%@ Page Title="Export Bookings" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="AdExport.aspx.vb" Inherits="BetterclassifiedAdmin.AdExport" %>

<%@ Register Src="~/classified/UserControls/BookingNavigation.ascx" TagName="BookingNavigation" TagPrefix="ucx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function openRadWindow(adID) 
        {
            var oWnd = radopen("ModalDialog/PreviewLineAdDetails.aspx?id=" + adID, "RadWindow1");
            oWnd.center();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyTitle" runat="server">
    Export Print Classified Bookings
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphUserNavigation" runat="server">
    <ucx:BookingNavigation ID="ucxBookingNavigation" runat="server" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphContentBody" runat="server">

    <telerik:RadAjaxManager ID="RadScriptManager" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="radComboPublication">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrintAds" LoadingPanelID="radAjaxLoading" />
                    <telerik:AjaxUpdatedControl ControlID="radComboEdition" LoadingPanelID="radAjaxLoading" />
                </UpdatedControls>
            </telerik:AjaxSetting>            
            <telerik:AjaxSetting AjaxControlID="radComboEdition">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrintAds" LoadingPanelID="radAjaxLoading" />
                </UpdatedControls>
            </telerik:AjaxSetting>            
            <telerik:AjaxSetting AjaxControlID="radDatePicker">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrintAds" LoadingPanelID="radAjaxLoading" />
                </UpdatedControls>
            </telerik:AjaxSetting> 
            <telerik:AjaxSetting AjaxControlID="grdPrintAds">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrintAds" LoadingPanelID="radAjaxLoading" />
                </UpdatedControls>
            </telerik:AjaxSetting>   
        </AjaxSettings>
    </telerik:RadAjaxManager>
    
    <telerik:RadAjaxLoadingPanel runat="server" ID="radAjaxLoading" BackgroundPosition="Center" Skin="WebBlue" />

    <h3>Export Print Classifieds</h3>
    <p>Select the publication and edition to export. Required XML will be forced to download to your local machine, and images 
    will be stored in the requested server path. You may update this path in 
    <asp:Hyperlink runat="server" ID="hyperlink1" Text="Application Settings" NavigateUrl="~/classified/ApplicationSettings.aspx"  /> under the name ImageStoragePath.</p>
        
    <%--Selection Criteria--%>    
 
            <table cellspacing="2">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Publication" Font-Bold="true"></asp:Label>:</td>
                </tr>
                <tr>
                    <td><telerik:RadComboBox ID="radComboPublication" runat="server" 
                        DataTextField="Title" DataValueField="PublicationId" 
                        AutoPostBack="true" CausesValidation="false" Skin="WebBlue" /></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Up-coming Edition" Font-Bold="true"></asp:Label>:</td>
                    <td></td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Specific Edition" Font-Bold="true"></asp:Label>:</td>
                </tr>
                <tr>
                    <td><telerik:RadComboBox ID="radComboEdition" runat="server" 
                            DataTextField="EditionDate" DataValueField="EditionDate"
                            DataTextFormatString="{0:dd/MM/yyyy}" AutoPostBack="true"
                            CausesValidation="false" Skin="WebBlue" /></td>
                    <td>OR</td>
                    <td><telerik:RadDatePicker ID="radDatePicker" runat="server" 
                            AutoPostBack="true" SkinID="WebBlue"  CausesValidation="false"/></td>
                    <td><asp:Button ID="btnSubmit" runat="server" Text="View Ads" Visible="false" /></td>
                </tr>
            </table>
       
            <div id="divResults" runat="server" style="width: 100%; margin-top:30px; padding-top: 10px; padding-bottom: 8px;">
                <%--Grid For Exporting--%>
                <telerik:RadGrid ID="grdPrintAds" 
                    Width="97%" AllowSorting="True" AllowPaging="True" 
                    Skin="WebBlue" AutoGenerateColumns="False" ShowFooter="true"
                    PageSize="20" AllowMultiRowSelection="True" runat="server" Gridlines="None">
                <ExportSettings IgnorePaging="true"  />
                 <MasterTableView Width="100%" Summary="RadGrid table">
                 
                    <Columns>
                        <telerik:GridBoundColumn DataField="iflogId" HeaderText="iFlog" />
                        <telerik:GridBoundColumn DataField="MainCategory" HeaderText="Category" />
                        <telerik:GridBoundColumn DataField="SubCategory" HeaderText="Sub Category" />
                        <telerik:GridBoundColumn DataField="NumOfWords" HeaderText="Word Count" Aggregate="Sum" FooterText="Total: " />
                        <telerik:GridBoundColumn DataField="UsePhoto" HeaderText="Has Photo"  />
                        <telerik:GridBoundColumn DataField="UseBoldHeader" HeaderText="Has Header"  />
                        <telerik:GridBoundColumn DataField="IsSuperBoldHeading" HeaderText="SuperBold"  />
                        <telerik:GridBoundColumn DataField="BoldHeadingColourCode" HeaderText="Header Colour"  />
                        <telerik:GridBoundColumn DataField="BackgroundColourCode" HeaderText="Background Colour"  />
                        <telerik:GridBoundColumn DataField="BorderColourCode" HeaderText="Border Colour"  />

                        <%--<telerik:GridImageColumn HeaderText="Image" DataType="System.String" DataImageUrlFields="UsePhoto" 
                            DataImageUrlFormatString="~/images/Status/status_{0}.png" ImageHeight="20" ImageWidth="20" />
                        <telerik:GridImageColumn HeaderText="Heading" DataType="System.String" DataImageUrlFields="UseBoldHeader" 
                            DataImageUrlFormatString="~/images/Status/status_{0}.png" ImageHeight="20" ImageWidth="20" />--%>

                        <telerik:GridBoundColumn DataField="UserId" HeaderText="Username" />
                        <telerik:GridTemplateColumn HeaderText="Preview">
                            <ItemTemplate>
                            <a href="#"
                                    onclick="openRadWindow('<%#DataBinder.Eval(Container.DataItem,"LineAdId")%>'); return false;">
                                <asp:Image ID="imgPreview" runat="server" ImageUrl="~/images/preview-icon.png" Height="20" Width="20" /></a>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Remove">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDelete" runat="server" 
                                    CommandName="Delete" CommandArgument='<%# Eval("BookEntryId") %>'
                                    ImageUrl="~/App_Themes/blue/images/delete.gif" 
                                    ToolTip="Remove"
                                    OnClientClick="javascript:return confirm('Deleting this edition entry will be removed permanently. Are you sure you want to continue?');" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                 </MasterTableView>
                 <PagerStyle Mode="NextPrevAndNumeric" />
                </telerik:RadGrid>
            </div>
       
    
    <div style="width: 96%; margin-top: 5px; padding: 2px;line-height: 20px;">
        Image Path: 
        <asp:TextBox ID="txtOutputPath" runat="server" Width="300px" Text="file:///user//Desktop/XML_CLassies/"></asp:TextBox>
        <asp:RequiredFieldValidator ID="valRequiredPath" runat="server" ControlToValidate="txtOutputPath" Text="*" />
        <div style="float:right;">
            <asp:Button ID="btnExport" runat="server" Text="Export" />
            <asp:Button ID="btnDownloadReport" runat="server" Text="Download Report" />
        </div>
    </div>
    
    <telerik:RadWindowManager ID="RadWindowManager1" VisibleOnPageLoad="false" 
        runat="server" Width="650px" Height="500px" Modal="true">
    </telerik:RadWindowManager>

</asp:Content>
