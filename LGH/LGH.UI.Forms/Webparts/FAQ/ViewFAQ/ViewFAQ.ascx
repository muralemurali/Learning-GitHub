<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewFAQ.ascx.cs" Inherits="LGH.UI.Forms.Webparts.FAQ.ViewFAQ.ViewFAQ" %>


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
					        <li>FAQ's</li>
				        </ul>
			        </div>
                    <div class="main_content">		
                        <h2>                            <span class="title">FAQ's</span>                            <span id="spanFaq" runat="server" class="addnew glyphicon glyphicon-plus-sign pull-right"><a href="/Pages/AddFaq.aspx" class="a_new">Add New</a></span>
                        </h2>
                        <asp:GridView runat="server" ID="gridFAQ" Width="100%" AutoGenerateColumns="False" GridLines="None"
                                CellSpacing="2" CssClass="sseGridStyles" HorizontalAlign="Center">
                            <Columns>
                                <asp:BoundField HeaderText="ID" DataField="ItemId" HeaderStyle-CssClass="GridHeaderStyle" ItemStyle-CssClass="GridItemStyle" />
                                <asp:TemplateField HeaderText="Question" HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="gridSponsorshipStyle thead th" ItemStyle-Width="45%" ItemStyle-HorizontalAlign="Left"> 
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTitle" Text='<%# Eval("Question") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Answer" HeaderStyle-Font-Bold="true" ItemStyle-Width="45%" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDescription" Text='<%# Eval("Answer") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                    
                    
                                <asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-Font-Bold="true" HeaderText="Actions" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="editItem" Text="" CommandArgument='<%#Eval("ItemId")%>'  OnClick="lnkEdit_Click" ToolTip="Edit This Item"></asp:LinkButton>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CssClass="deleteItem" Text="" CommandArgument='<%#Eval("ItemId")%>' ToolTip="Delete This Item"  OnClick="lnkDelete_Click" OnClientClick="return fnConfirm();"></asp:LinkButton>
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

<asp:HiddenField ID="hdnitemid" runat="server" />