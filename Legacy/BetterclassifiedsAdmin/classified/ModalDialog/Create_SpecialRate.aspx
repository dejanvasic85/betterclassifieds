<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Create_SpecialRate.aspx.vb" Inherits="BetterclassifiedAdmin.Create_SpecialRate" %>

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
                
                <div style="padding: 10px;">
                   
                    <asp:Panel ID="pnlDetails" runat="server">
                    <asp:DetailsView ID="dtlGeneralInfo" runat="server" AutoGenerateRows="false" HeaderText="General Info"
                                     DefaultMode="Insert" DataKeyNames="BaseRateId" DataSourceID="linqSourceGeneral">
                        <Fields>
                            <asp:BoundField HeaderText="Name" DataField="Title" ControlStyle-Width="200px" />
                            <asp:TemplateField HeaderText="Description" ItemStyle-VerticalAlign="Top" HeaderStyle-VerticalAlign="Top" ControlStyle-Width="200px">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDescription" runat="server" DataField="Description" Text='<%# Eval("Description") %>'
                                                 TextMode="MultiLine" Rows="6" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Fields>
                    </asp:DetailsView>
                    <asp:LinqDataSource ID="linqSourceGeneral" runat="server" EnableObjectTracking="false"
                                        ContextTypeName="BetterclassifiedsCore.DataModel.BetterclassifiedsDataContext" 
                                        TableName="BaseRates" EnableInsert="true" />
                    
                    <br /><br />
                   
                    
                    
                    <asp:DetailsView ID="dtlSpecialRate" runat="server" AutoGenerateRows="false" HeaderText="Special Details"
                                     DefaultMode="Insert" DataKeyNames="SpecialRateId" DataSourceID="linqSourceSpecialRate">
                        <Fields>                          
                            <asp:BoundField HeaderText="Set Price" DataField="SetPrice" />
                            <asp:BoundField HeaderText="Free Insertions/Editions" DataField="NumOfInsertions" />
                            <asp:BoundField HeaderText="Maximum Words" DataField="MaximumWords" />
                            <asp:BoundField HeaderText="Discount" DataField="Discount" />
                            <asp:CheckBoxField HeaderText="Include Line Ad Bold Heading" DataField="LineAdBoldHeader" />
                            <asp:CheckBoxField HeaderText="Include Line Ad Image" DataField="LineAdImage" />
                        </Fields>
                    </asp:DetailsView>
                    
                    <asp:LinqDataSource ID="linqSourceSpecialRate" runat="server" EnableObjectTracking="false"
                                        ContextTypeName="BetterclassifiedsCore.DataModel.BetterclassifiedsDataContext" 
                                        TableName="SpecialRates" EnableInsert="true" >               
                    </asp:LinqDataSource>
                    
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
