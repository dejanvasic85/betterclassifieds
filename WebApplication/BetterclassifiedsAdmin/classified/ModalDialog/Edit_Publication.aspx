<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Edit_Publication.aspx.vb"
    Inherits="BetterclassifiedAdmin.Edit_Publication" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Publication Details</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 20px">
        <div style="font-size: 11px;">
            <br />
            <asp:DetailsView ID="viewPublications" runat="server" AutoGenerateRows="False" Height="50px"
                Style="width: 445px" HeaderText="General Publication Details" DefaultMode="Edit"
                DataSourceID="linqPublicationUpdate" DataKeyNames="PublicationId">
                <Fields>
                    <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                    <asp:TemplateField HeaderText="Publication Type">
                        <EditItemTemplate>
                            <asp:Label ID="lblPublicationType" runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDescription" runat="server" DataField="Description" TextMode="MultiLine"
                                Rows="10" Width="300px" Text='<%# Eval("Description") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Image Name">
                        <EditItemTemplate>
                            <asp:FileUpload ID="fileUploadImage" runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
                    <asp:BoundField HeaderText="SortOrder" DataField="SortOrder" ControlStyle-Width="50px" />
                </Fields>
            </asp:DetailsView>
            <asp:LinqDataSource ID="linqPublicationUpdate" runat="server" ContextTypeName="BetterclassifiedsCore.DataModel.BetterclassifiedsDataContext"
                TableName="Publications" EnableUpdate="true">
            </asp:LinqDataSource>
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
                        <asp:Label ID="lblFrequencyType" runat="server" />
                    </td>
                </tr>
                <tr id="trWeeklyDetails" runat="server" class="detailsviewRowStyle">
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
            
            <div style="padding: 10px;" runat="server" id="divImage" visible="false">
                <asp:Image ID="imgDisplay" runat="server" />
                <br />
                <asp:LinkButton ID="lnkRemove" runat="server" Text="Remove Image" />
            </div>
            
        </div>
        <br />
        <div style="float: right">
            <asp:Button ID="btnUpdatePublication" runat="server" Text="Update" />
        </div>
        <br />
        <div>
            <asp:Label ID="UpdateMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
        </div>
    </div>
    </form>
</body>
</html>
