<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewReport.ascx.cs" Inherits="LGH.UI.Forms.Webparts.LGHREPORTS.ViewReport.ViewReport" %>


<script type="text/javascript">

    function showSPDialog(pageUrl, lnk) {

        var row = lnk.parentNode.parentNode;
        var itemId = row.cells[0].innerHTML;

        pageUrl = pageUrl + "?ItemId=" + itemId;

        var options = {
            url: pageUrl,
            title: '',
            width: 650,
            height: 475,
            allowMaximize: false,
            showClose: true,
            dialogReturnValueCallback: closeDialogCallBack
        };
        SP.SOD.execute('sp.ui.dialog.js', 'SP.UI.ModalDialog.showModalDialog', options);
    }

    function closeDialogCallBack(dialogResult, returnValue) {
        location.reload(true);
    }

</script>

<style type="text/css">
    .GridHeaderStyle
    {
	    display: none;
    }
    .GridItemStyle
    {
	    display: none;
    }
</style>

<div>
    <asp:UpdatePanel runat="server" ID="updPanel">
        <ContentTemplate>
            
                    <div class="breadcrumbs">
				        <ul class="breadcrumb_list">
					        <li><a href="/Pages/LGHome.aspx">Home</a><span>></span></li>
					        <li>Reports</li>
				        </ul>
			        </div>
                    <div class="main_content">		
                        <h2>                            <span class="title">Reports</span>                            <span id="spanCarousel" runat="server" class="addnew glyphicon glyphicon-plus-sign pull-right"><a href="/Pages/AddReports.aspx" class="a_new">Add New</a></span>
                        </h2>
                        <asp:GridView runat="server" ID="gridCarousel" Width="100%" AutoGenerateColumns="False" GridLines="None"
                                CellSpacing="2" CssClass="sseGridStyles" OnRowDataBound="gridCarousel_RowDataBound">
                            <Columns>
                                <asp:BoundField HeaderText="ID" DataField="ItemId" HeaderStyle-CssClass="GridHeaderStyle" ItemStyle-CssClass="GridItemStyle" />
                                  
                                <asp:TemplateField HeaderText="Title" ItemStyle-Width="20%">
                                    <ItemTemplate>

                                        <asp:Label runat="server" ID="lbltitle" Text='<%# Eval("Url") %>' Visible="false"></asp:Label>
                                        <asp:HyperLink ID="hpltitle"  runat="server" Text='' ForeColor="Blue" Font-Underline="true" ></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDescription" Text='<%# Eval("shortNotes") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              
                    
                                <asp:TemplateField HeaderText="Actions" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="editItem"  Text="" CommandArgument='<%#Eval("ItemId")%>'  OnClick="lnkEdit_Click" ToolTip="Edit this Item"></asp:LinkButton>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CssClass="deleteItem"  Text ="" CommandArgument='<%#Eval("ItemId")%>' OnClick="lnkDelete_Click" OnClientClick="return fnConfirm();" ToolTip="Delete this Item"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle CssClass="sseGridStylesRow" />     
                        </asp:GridView> 
                    </div>
                
        </ContentTemplate>
    </asp:UpdatePanel>
</div>

<script type="text/javascript">
    function fnConfirm() {
        if (confirm("Do you want to delete the item?") == true)
            return true;
        else
            return false;
    }
</script>