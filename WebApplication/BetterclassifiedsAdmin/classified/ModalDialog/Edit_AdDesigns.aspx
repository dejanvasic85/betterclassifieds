<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Edit_AdDesigns.aspx.vb" Inherits="BetterclassifiedAdmin.Edit_AdDesigns" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="modalContainer">
        <%-- ajax script manager --%>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
        <asp:Label ID="lblFlogId" runat="server" CssClass="ifloglabel" />
        
        <telerik:RadTabStrip runat="server" ID="radTabStrip" MultiPageID="radMultiPage" SelectedIndex="0">
            <Tabs>
                <telerik:RadTab Text="Line Ad Details" />               
                <telerik:RadTab Text="Online Ad Details" />
                <telerik:RadTab Text="Images" />
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="radMultiPage" runat="server" SelectedIndex="0">
            <telerik:RadPageView ID="radPageLineAd" runat="server" CssClass="multipage">
                
                <asp:Label ID="lblLineMessage" runat="server" />
                
                <paramountIt:LineAdEditDetails runat="server" ID="lineAdDetails"
                    CssClass="lineAdEdit" IsAdminMode="true" 
                    QueryParamType="AdBookingId" QueryParamName="adBookingId"
                    style="display:block; padding: 10px;" CancelButtonVisible="false" />
            </telerik:RadPageView>
            
            <%--Online Ad Details Tab--%>
            <telerik:RadPageView ID="radPageOnlineAd" runat="server" CssClass="multipage">
              
                <asp:Label ID="lblOnlineMessage" runat="server" />
                
                <asp:DetailsView ID="dtlOnlineAd" runat="server" AutoGenerateRows="False"
                    style="width: 90%; margin-top: 10px; margin-left: 10px;" HeaderText="Online Ad Details"
                    DefaultMode="Edit" DataKeyNames="OnlineAdId" DataSourceID="linqSourceOnlineAd">
                    <Fields>
                        <asp:BoundField DataField="OnlineAdId" HeaderText="Ad Id" ReadOnly="true" Visible="false" />
                        <asp:BoundField DataField="AdDesignId" HeaderText="iFlog ID" ReadOnly="true" Visible="false"/>
                        <asp:BoundField DataField="Price" HeaderText="Price" />
                        <asp:BoundField DataField="ContactName" HeaderText="Contact Name" />
                        <asp:TemplateField HeaderText="Contact Type">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlContactType" runat="server" DataValueField='<%# Eval("ContactType") %>'>
                                    <asp:ListItem>Email</asp:ListItem>
                                    <asp:ListItem>Phone</asp:ListItem>
                                    <asp:ListItem>Fax</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ContactValue" HeaderText="Contact Detail" />
                        <asp:BoundField DataField="Heading" HeaderText="Heading" ControlStyle-Width="250px" />
                        <asp:TemplateField HeaderText="HTML Text">
                            <ItemTemplate>
                                <telerik:RadEditor ID="radEditor" runat="server" Width="100%" Height="300px"
                                    NewLineBr="true" ToolbarMode="Default" EditModes="All">
                                    <Tools>
                                        <telerik:EditorToolGroup Tag="EditToolbar">
                                            <telerik:EditorTool Name="Print" Text="Print" />
                                        </telerik:EditorToolGroup>
                                        <telerik:EditorToolGroup Tag="FormatToolbar">
                                            <telerik:EditorTool name="FontName" text="Font" />
                                            <telerik:EditorTool name="FontSize" text="Font Size" />
                                            <telerik:EditorTool name="ForeColor" text="Font Color" />
                                            <telerik:EditorTool name="BackColor" text="Background Color" />
                                            <telerik:EditorTool name="Bold" text="Bold" />
                                            <telerik:EditorTool name="Italic" text="Italic" />
                                            <telerik:EditorTool name="Underline" text="Underline" />
                                        </telerik:EditorToolGroup>
                                        <telerik:EditorToolGroup>
                                            <telerik:EditorTool name="JustifyLeft" text="JustifyLeft" />
                                            <telerik:EditorTool name="JustifyCenter" text="JustifyCenter" />
                                            <telerik:EditorTool name="JustifyRight" text="JustifyRight" />
                                            <telerik:EditorTool name="JustifyFull" text="JustifyFull" />
                                            <telerik:EditorTool name="Indent" text="Indent" />
                                            <telerik:EditorTool name="Outdent" text="Outdent" />
                                        </telerik:EditorToolGroup>
                                        <telerik:EditorToolGroup>
                                            <telerik:EditorTool name="InsertUnorderedList" text="InsertUnorderedList" />
                                            <telerik:EditorTool name="InsertOrderedList" text="InsertOrderedList" />
                                        </telerik:EditorToolGroup>
                                    </Tools>
                                </telerik:RadEditor>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Fields>
                </asp:DetailsView>
                
                <table width="90%" class="detailsviewMain" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="2" class="detailsviewheaderBG">Location Details</td>
                    </tr>
                    <tr>
                        <td style="width: 170px;"><asp:Label ID="Label2" runat="server" Text="Location"></asp:Label></td>
                        <td><asp:DropDownList ID="ddlLocation" runat="server" DataTextField="Title" DataValueField="LocationId" AutoPostBack="true" Width="200px" /></td>
                    </tr>
                    <tr>
                        <td style="width: 170px;">Location Area</td>
                        <td><asp:DropDownList ID="ddlLocationArea" runat="server" DataTextField="Title" DataValueField="LocationAreaId" Width="200px" /></td>
                    </tr>

                </table>
                
                <asp:LinqDataSource ID="linqSourceOnlineAd" runat="server" EnableUpdate="true" EnableObjectTracking="false"
                     ContextTypeName="BetterclassifiedsCore.DataModel.BetterclassifiedsDataContext" TableName="OnlineAds" />
                
                <div class="controlDiv">
                    <asp:Button ID="btnUpdateOnline" runat="server" Text="Update" />
                </div>     
            </telerik:RadPageView>
            
            <%--Image Tab--%>
            <telerik:RadPageView ID="radPageImages" runat="server" CssClass="multipage">
                <div class="uploadManager">
                    <%--<div style="margin-left:10px; margin-top:5px;">
                        <telerik:RadProgressManager ID="RadProgressManager2" runat="server"
                            ProgressIndicators="FilesCountBar,FilesCountPercent,TimeElapsed,TimeEstimated,CurrentFileName" />
                        <telerik:RadProgressArea ID="RadProgressArea2" runat="server" />
                    </div>--%>

                    <asp:Panel ID="pnlOnline" runat="server">
                        <h1>Online Ad Images</h1>
                        <div class="breakSmall"></div>
                        <paramountIt:MultipleFileUpload ID="paramountFileUpload" runat="server" DocumentCategory="OnlineAd" IsUploadOnSelect="true" />
                        <div class="breakLarge"></div>
                    </asp:Panel>
                
                    <asp:Panel ID="pnlPrint" runat="server">
                        <h1><asp:Label ID="lblPrintTitle" runat="server" Text="Print Image" /></h1>
                        <div class="breakSmall"></div>
                        <paramountIt:WebImageMaker ID="paramountWebImageMaker" runat="server" DocumentCategory="LineAd"
                            CancelButtonText="Cancel" ConfirmButtonText="Done" UploadButtonText="Upload" ImageUrl=""
                            WorkingDirectory="C:\Paramount\WebImageMakerCache\" Format="Jpg" Quality="High" />
                        <div class="breakLarge"></div>
                    </asp:Panel>
                </div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
    </form>
</body>
</html>
