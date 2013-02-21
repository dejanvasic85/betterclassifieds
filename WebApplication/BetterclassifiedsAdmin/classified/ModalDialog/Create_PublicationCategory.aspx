<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Create_PublicationCategory.aspx.vb" Inherits="BetterclassifiedAdmin.Create_PublicationCategory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
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
                
                <div style="padding: 10px;">
                   
                    <asp:Panel ID="pnlDetails" runat="server">
                    
                        <asp:DetailsView    ID="dtlPublicationCategory" runat="server" HeaderText="General Category Details"
                                            DefaultMode="Insert" AutoGenerateRows="false" DataKeyNames="PublicationCategoryId"
                                            DataSourceID="srcPublicationCategory">
                            
                            <Fields>
                                <asp:BoundField DataField="Title" HeaderText="Name" ControlStyle-Width="200" />
                                <asp:TemplateField HeaderText="Description">
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="txtDescription" runat="server" DataField="Description" Text='<%# Eval("Description") %>'
                                                 TextMode="MultiLine" Rows="5" Width="200" />
                                    </InsertItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ImageUrl" HeaderText="Image Url" ControlStyle-Width="100" />
                                <asp:TemplateField HeaderText="Main Category">
                                    <InsertItemTemplate>
                                        <asp:DropDownList ID="ddlMainCategory" runat="server" DataField="MainCategoryId"
                                                            DataTextField="Title" DataValueField="MainCategoryId" />
                                    </InsertItemTemplate>
                                </asp:TemplateField>
                            </Fields>
                        
                        </asp:DetailsView>
                        
                        <asp:LinqDataSource ID="srcPublicationCategory" runat="server" EnableObjectTracking="false"
                                        ContextTypeName="BetterclassifiedsCore.DataModel.BetterclassifiedsDataContext" 
                                        TableName="PublicationCategories" EnableInsert="true" />
                        
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
                    
                        <div style="float:right; margin-top:10px;">
                            <asp:Button ID="btnCreate" runat="server" Text="Create" /></div><br />
                    </asp:Panel>
                        
                    <div>
                        <asp:Label ID="msgInsert" runat="server" Text="" ForeColor="Red"></asp:Label></div>
                        
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
