<%@ Page Title="iFlog Search Results" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Default.Master"
    CodeBehind="Default.aspx.vb" Inherits="BetterclassifiedsWeb._Default5" %> 
<%@ Register Src="~/Controls/Search/KeySearch.ascx" TagName="KeySearch" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/Search/FlogID.ascx" TagName="FlogSearch" TagPrefix="ucx" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div id="contentMainHead">
        
        <div id="searchTotalContent">
            <ucx:KeySearch ID="ucxKeySearch" runat="server" />
        </div>
    </div>
    
    <div class="clearFloat">
        <div id="contentBody">
            <div id="sidebar">
                <div id="sidebarHeader">
                    <p>
                        categories header</p>
                </div>
                <div id="sidebarContent">
                        <paramountIt:CategorySelector ID="categorySelector" runat="server"  />
                </div>
                <ucx:FlogSearch ID="ucxFlogSearch" runat="server" />
            </div>
            
            <div id="mainSearchResults">
                <div id="mainHeaderSearchResults">
                    
                    <h2>
                        <asp:Label ID="lblSearchResults" runat="server" Text="" /></h2>
                    <%--<h3>
                        25 Results Found</h3>--%>
                </div>
                <div id="searchBlue">
                    <div class="showingResults">
                        <p>
                            <%--Showing 1 - 10 of 25 ads for &quot;example example&quot;--%>
                            <asp:Label ID="lblSearchDetails" runat="server" /></p>
                    </div>
                    <div class="sortByResults">
                        <%--<p>
                            Sort by:</p>--%>
                    </div>
                   <%-- <div class="sortByDropdownBox" style="margin-top:2px;">
                        <asp:DropDownList ID="ddlSortBy" runat="server" AutoPostBack="true">
                            <asp:ListItem Selected="True" value="RowNumber asc">Latest First</asp:ListItem>
                            <asp:ListItem value="RowNumber DESC">Oldest First</asp:ListItem>
                            <asp:listitem text="Cost" value ="Price asc" />
                            <asp:listitem text="Location" value="Location asc" />
                        </asp:DropDownList>
                    </div>--%>
                </div>
                <div id="searchTableBody">
                    <paramountit:paramountgrid ID="grdSearchResults" runat="server" ShowHeader="true"
                            Width="751" BorderStyle="None" CellPadding="4" CellSpacing="0"
                            AutoGenerateColumns="False" AllowPaging="True" GridLines="Horizontal" AllowSorting="true"
                            PageSize="10">
                            <pagersettings position="Bottom"  mode="NumericFirstLast"   />
                        <HeaderStyle Height="28px" HorizontalAlign="Left" CssClass="searchTableItemHead" />
                        <EmptyDataTemplate>
                            <h5>No result found</h5>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="Item" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnOnlineAdId" runat="server" Value='<%# Eval("OnlineAdId") %>' />
                                    <table width="376" border="0" cellspacing="0" cellpadding="3">
                                        <tr>
                                            <td colspan="2">
                                                <div id="categoryBodySearchResults">
                                                    <asp:HyperLink ID="HyperLink1" runat="server" 
                                                            Text='<%# Eval("Heading") %>' Font-Bold="true"
                                                            NavigateUrl='<%# String.Format("{0}{1}", "~/OnlineAds/AdView.aspx?preview=false&id=", Eval("OnlineAdId")) %>' />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td rowspan="2" width="90">
                                                <asp:HyperLink ID="imgDocument" runat="server" 
                                                    ImageUrl='<% Eval("ImageUrl") %>'
                                                    NavigateUrl='<%# String.Format("{0}{1}", "~/OnlineAds/AdView.aspx?preview=false&id=", Eval("OnlineAdId")) %>' />   
                                            </td>
                                            <td width="286" height="48">
                                                <div id="categoryBodySearchResults">
                                                    <p>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("AdText") %>'  /></p>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="286" height="12">
                                                <div id="categoryMoreSearchResults">
                                                    <p>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# String.Format("{0}{1}{2}", "Viewed: ", Eval("NumOfViews"), " times") %>' /></p>
                                                </div>
                                                <div id="categoryMoreSearchResultsButton">
                                                    <p>
                                                        <asp:HyperLink ID="HyperLink3" runat="server" 
                                                            Text="DETAILS"
                                                            NavigateUrl='<%# String.Format("{0}{1}", "~/OnlineAds/AdView.aspx?preview=false&id=", Eval("OnlineAdId")) %>' />
                                                            
                                                            </p>
                                                            
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Price">
                                <ItemTemplate>
                                    <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Location" HeaderText="Location" 
                                ItemStyle-HorizontalAlign="Center"  />
                            <%--<asp:BoundField DataField="Category" ItemStyle-HorizontalAlign="Center" />--%>
                            <asp:BoundField DataField="Posted" HeaderText="Posted" DataFormatString="{0:dd/MM/yy}" 
                                ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                    </paramountit:paramountgrid >
                   <div class="grid-pager" >
                  <%--  <asp:datapager id="DataPager1" runat="server" >
                        <fields >
                            <
                        </fields>
                    </asp:datapager>--%>
                   </div>
                    <asp:ObjectDataSource ID="objSourceResults" runat="server"
                            MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                            SelectMethod="SearchOnlineAds" SelectCountMethod="SearchOnlineAdsCount"
                            TypeName="BetterclassifiedsCore.DataModel.Search" 
                            OldValuesParameterFormatString="original_{0}" EnablePaging="True">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="category" QueryStringField="category" Type="Int32" />
                            <asp:QueryStringParameter Name="subCategoryId" QueryStringField="subCategory" Type="Int32" />
                            <asp:QueryStringParameter Name="locationId" QueryStringField="location" Type="Int32" />
                            <asp:QueryStringParameter Name="areaId" QueryStringField="area" Type="Int32" />
                            <asp:QueryStringParameter Name="keyword" QueryStringField="w" Type="String" />
                            <asp:Parameter Name="startRowIndex" Type="Int32" />
                            <asp:Parameter Name="maximumRows" Type="Int32" />
                        </SelectParameters>
                        
                    </asp:ObjectDataSource>
                </div>
            </div>
            
        </div>
    </div>
    
</asp:Content>
