<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Edit_Location.aspx.vb" Inherits="BetterclassifiedAdmin.Edit_Location" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 20px">
        <%-- ajax script manager --%>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
        <%-- ajax update panel start --%>
        <asp:UpdatePanel ID="pnlUpdate" runat="server">
            <ContentTemplate>
                
                <ajax:TabContainer ID="tcntLocation" runat="server" ActiveTabIndex="0" 
                    Width="100%" Font-Size="10px">
                    
                    <ajax:TabPanel ID="TabPanel2" runat="server" HeaderText="General Info">
                        <ContentTemplate>
                        <div style="font-size: 11px;">
                            <br />
                            
                            <asp:DetailsView ID="dtlLocation" runat="server" AutoGenerateRows="False"
                                style="width: 445px" HeaderText="Location Details"
                                DefaultMode="Edit" DataKeyNames="LocationId" DataSourceID="linqSource">
                                <Fields>
                                    <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                    <asp:TemplateField HeaderText="Description">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDescription" DataField="Description" runat="server" Rows="5" TextMode="MultiLine" Text='<%# Eval("Description") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Fields>
                            </asp:DetailsView>
                            <asp:LinqDataSource ID="linqSource" runat="server" EnableUpdate="true" EnableObjectTracking="false"
                                 ContextTypeName="BetterclassifiedsCore.DataModel.BetterclassifiedsDataContext" TableName="Locations" />
                        </div>
                        </ContentTemplate>
                    </ajax:TabPanel>
                </ajax:TabContainer>
                
                <div class="controlDiv">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" />
                </div>
                
                <br />
                
                <div>
                    <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red" />
                </div>
                
            </ContentTemplate>
        </asp:UpdatePanel>
        
        <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="pnlUpdate">
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
