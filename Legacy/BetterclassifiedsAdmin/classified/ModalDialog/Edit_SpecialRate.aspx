<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Edit_SpecialRate.aspx.vb" Inherits="BetterclassifiedAdmin.Edit_SpecialRate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 20px;">
        <%-- ajax script manager --%>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
        <%-- ajax update panel start --%>
        <asp:UpdatePanel ID="pnlUpdateGeneralDetails" runat="server">
            <ContentTemplate>
                <ajax:TabContainer ID="tcntUserInfo" runat="server" ActiveTabIndex="0" 
                    Width="100%" Font-Size="10px">
                    
                    <ajax:TabPanel ID="TabPanel2" runat="server" HeaderText="General Info">
                        <ContentTemplate>
                            <div style="padding: 10px;">
                            
                                <asp:DetailsView ID="dtlGeneralInfo" runat="server" AutoGenerateRows="false" HeaderText="General Info"
                                                 DefaultMode="Edit" DataKeyNames="BaseRateId" DataSourceID="linqSourceGeneral">
                                    <Fields>
                                    
                                        <asp:TemplateField HeaderText="Description" ItemStyle-VerticalAlign="Top" HeaderStyle-VerticalAlign="Top" ControlStyle-Width="200px">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDescription" runat="server" DataField="Description" Text='<%# Eval("Description") %>'
                                                             TextMode="MultiLine" Rows="6" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Name" DataField="Title" ControlStyle-Width="200px" />
                                        
                                    </Fields>
                                </asp:DetailsView>
                                <asp:LinqDataSource ID="linqSourceGeneral" runat="server" EnableUpdate="true" EnableObjectTracking="false"
                                                    ContextTypeName="BetterclassifiedsCore.DataModel.BetterclassifiedsDataContext" 
                                                    TableName="BaseRates" />
                                
                                <br /><br />
                                <asp:DetailsView ID="dtlSpecialRate" runat="server" AutoGenerateRows="false" HeaderText="Special Rate Details"
                                                 DefaultMode="Edit" DataKeyNames="SpecialRateId" DataSourceID="linqSourceSpecial">
                                    <Fields>
                                                                              
                                        <asp:BoundField HeaderText="Set Price" DataField="SetPrice" />
                                        <asp:BoundField HeaderText="Maximum Words" DataField="MaximumWords" />
                                        <asp:BoundField HeaderText="Insertions/Editions" DataField="NumOfInsertions" />
                                        <asp:BoundField HeaderText="Discount" DataField="Discount" />
                                        <asp:CheckBoxField HeaderText="Include Line Ad Bold Heading" DataField="LineAdBoldHeader" />
                                        <asp:CheckBoxField HeaderText="Include Line Ad Image" DataField="LineAdImage" />
                                        
                                    </Fields>
                                </asp:DetailsView>
                                
                                <asp:LinqDataSource ID="linqSourceSpecial" runat="server" EnableUpdate="true" EnableObjectTracking="false"
                                                    ContextTypeName="BetterclassifiedsCore.DataModel.BetterclassifiedsDataContext" 
                                                    TableName="SpecialRates" />
                                
                                <div style="float:right; margin-top:10px;">
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" /></div><br />
                                    
                                <div>
                                    <asp:Label ID="UpdateMessage" runat="server" Text="" ForeColor="Red"></asp:Label></div>
                            </div>
                        </ContentTemplate>
                    </ajax:TabPanel>
                    
                </ajax:TabContainer>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
