<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Create_Ratecard.aspx.vb" Inherits="BetterclassifiedAdmin.Create_Ratecard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Create New Ratecard</title>
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
                    <span class="spanWarning">
                        <asp:Label ID="Label1" runat="server" Text="Note" Font-Bold="true" />: &nbsp;
                        <asp:Label ID="Label2" runat="server" Text="Online ratecard makes use of only the Minimum Charge value."></asp:Label>
                    </span>
                    <br />
                    <asp:DetailsView ID="dtlRatecard" runat="server" AutoGenerateRows="false" HeaderText="Rate Details"
                                     DefaultMode="Insert" DataKeyNames="RatecardId" DataSourceID="linqSourceRatecard">
                        <Fields>                          
                            <asp:BoundField HeaderText="Minimum Charge" DataField="MinCharge" DataFormatString="{0:F2}" HtmlEncode="true"  ConvertEmptyStringToNull="true" />
                            <asp:BoundField HeaderText="MaxCharge" HtmlEncode="false" DataField="MaxCharge" DataFormatString="{0:f2}"  ConvertEmptyStringToNull="true" />
                            <asp:BoundField HeaderText="Free Words" DataField="MeasureUnitLimit" ConvertEmptyStringToNull="true" />
                            <asp:BoundField HeaderText="Rate Per Word" DataField="RatePerMeasureUnit"  ConvertEmptyStringToNull="true" />
                            <asp:BoundField HeaderText="Photo Charge" DataField="PhotoCharge"  ConvertEmptyStringToNull="true" />
                            <asp:BoundField HeaderText="Bold Heading Charge" DataField="BoldHeading"  ConvertEmptyStringToNull="true" />
                        </Fields>
                    </asp:DetailsView>
                    
                    <asp:LinqDataSource ID="linqSourceRatecard" runat="server" EnableObjectTracking="false"
                                        ContextTypeName="BetterclassifiedsCore.DataModel.BetterclassifiedsDataContext" 
                                        TableName="Ratecards" EnableInsert="true" >               
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
