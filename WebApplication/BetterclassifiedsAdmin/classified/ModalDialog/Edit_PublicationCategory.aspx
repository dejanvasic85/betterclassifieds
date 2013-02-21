<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Edit_PublicationCategory.aspx.vb" Inherits="BetterclassifiedAdmin.Edit_PublicationCategory" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 20px">
        <%-- ajax script manager --%>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
        <%-- ajax update panel start --%>
        <asp:UpdatePanel ID="pnlUpdateGeneralDetails" runat="server">
            <ContentTemplate>
                
                <asp:Panel ID="pnlControls" runat="server">
                    <ajax:TabContainer ID="tcntCategoryInfo" runat="server" ActiveTabIndex="0" 
                        Width="100%" Font-Size="10px">
                        
                        <ajax:TabPanel ID="TabPanel2" runat="server" HeaderText="General Info">
                            <ContentTemplate>
                            <div style="font-size: 11px;">
                                <br />
                                
                                <asp:DetailsView ID="dtlPublicationCategory" runat="server" AutoGenerateRows="False"
                                    style="width: 445px" HeaderText="Category Details"
                                    DefaultMode="Edit" DataKeyNames="PublicationCategoryId" DataSourceID="lnqCategorySource">
                                    <Fields>
                                        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                        <asp:BoundField DataField="ImageUrl" HeaderText="Image" />
                                        <asp:TemplateField HeaderText="Description">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDescription" runat="server" DataField="Description" 
                                                    TextMode="MultiLine" Rows="10" Width="300px" Text='<%# Eval("Description") %>'  />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                    </Fields>
                                </asp:DetailsView>
                                
                                <asp:LinqDataSource ID="lnqCategorySource" runat="server" EnableUpdate="true" EnableObjectTracking="false"
                                     ContextTypeName="BetterclassifiedsCore.DataModel.BetterclassifiedsDataContext" 
                                     TableName="PublicationCategories" />
                                     
                                <br />
                                
                                <asp:Panel ID="pnlRateDetails" runat="server">
                                    <table width="100%" class="detailsviewMain" cellpadding="0">
                                        <tr>
                                            <td colspan="2" class="detailsviewheaderBG">
                                                <asp:Label ID="Label1" runat="server" Text="Pricing"></asp:Label></td>
                                        </tr>
                                        <tr class="detailsviewRowStyle">
                                            <td style="width: 170px;">
                                                <asp:Label ID="Label2" runat="server" Text="Ratecard"></asp:Label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlRatecard" runat="server" DataTextField="Title" DataValueField="RatecardId" />
                                            </td>
                                        </tr>
                                        <tr class="detailsviewRowStyle">
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Text="Special Rate"></asp:Label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlSpecialRate" runat="server" DataTextField="Title" DataValueField="SpecialRateId" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                     
                            </div>
                            </ContentTemplate>
                        </ajax:TabPanel>
                    </ajax:TabContainer>
                    
                    <div class="controlDiv">
                        <asp:Button ID="btnUpdateCategory" runat="server" Text="Update" />
                    </div>
                </asp:Panel>
                
                <br />
                
                <div>
                    <asp:Label ID="lblCategoryMsg" runat="server" ForeColor="Red" />
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
    </form>
</body>
</html>
