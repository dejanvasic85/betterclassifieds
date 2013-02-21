<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Create_Publication.aspx.vb"
    Inherits="BetterclassifiedAdmin.Create_Publication" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 20px">
        <%-- ajax script manager --%>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div style="padding: 10px;">
            <asp:Panel ID="pnlDetails" runat="server">
                <asp:DetailsView ID="dtlPublication" runat="server" HeaderText="General Publication Details"
                    DefaultMode="Insert" AutoGenerateRows="false" DataKeyNames="PublicationCategoryId"
                    DataSourceID="srcPublicationCategory">
                    <Fields>
                        <asp:BoundField HeaderText="Title" DataField="Title" />
                        <asp:TemplateField HeaderText="Description" HeaderStyle-VerticalAlign="Top">
                            <InsertItemTemplate>
                                <asp:TextBox ID="txtDescription" runat="server" DataField="Description" Text='<%# Eval("Description") %>'
                                    TextMode="MultiLine" Rows="5" Width="200" />
                            </InsertItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Publication Type">
                            <InsertItemTemplate>
                                <asp:DropDownList ID="ddlPublicationType" runat="server" DataField="PublicationTypeId"
                                    DataValueField="PublicationTypeId" DataTextField="Title" />
                            </InsertItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Image">
                            <ItemTemplate>
                                <asp:FileUpload ID="fileUploadImage" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CheckBoxField HeaderText="Active" DataField="Active" />
                        <asp:BoundField HeaderText="SortOrder" DataField="SortOrder" ControlStyle-Width="50px" />
                    </Fields>
                </asp:DetailsView>
                <asp:LinqDataSource ID="srcPublicationCategory" runat="server" EnableObjectTracking="false"
                    ContextTypeName="BetterclassifiedsCore.DataModel.BetterclassifiedsDataContext"
                    TableName="Publications" EnableInsert="true" />
                <table width="100%" class="detailsviewMain" cellpadding="0">
                    <tr>
                        <td colspan="2" class="detailsviewheaderBG">
                            <asp:Label ID="Label1" runat="server" Text="Frequency"></asp:Label>
                        </td>
                    </tr>
                    <tr class="detailsviewRowStyle">
                        <td style="width: 170px;">
                            <asp:Label ID="Label2" runat="server" Text="Type"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFrequencyType" runat="server" AutoPostBack="true">
                                <asp:ListItem>Online</asp:ListItem>
                                <asp:ListItem Selected="True">Weekly</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr runat="server" id="trWeekly" class="detailsviewRowStyle">
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Edition Day"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlWeekDay" runat="server">
                                <asp:ListItem Text="Monday" Value="1" />
                                <asp:ListItem Text="Tuesday" Value="2" />
                                <asp:ListItem Text="Wednesday" Value="3" />
                                <asp:ListItem Text="Thursday" Value="4" />
                                <asp:ListItem Text="Friday" Value="5" />
                                <asp:ListItem Text="Saturday" Value="6" />
                                <asp:ListItem Text="Sunday" Value="7" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <div style="float: right; margin-top: 10px;">
                    <asp:Button ID="btnCreate" runat="server" Text="Create" /></div>
                <br />
            </asp:Panel>
            <div>
                <asp:Label ID="msgInsert" runat="server" Text="" ForeColor="Red"></asp:Label></div>
        </div>
    </div>
    </form>
</body>
</html>
