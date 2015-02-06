<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SFDirectory.ascx.cs" Inherits="LGH.StaffDirectory.WebParts.StaffDirectory.SFDirectory.SFDirectory" %>







<table style="width:100%" class="staffsearch">
    <tr>
        <td>
            <div class="form-group">
            <label for="title">NAME:</label>
            <asp:TextBox ID="txtname" runat="server" Text="" CssClass="form-control"></asp:TextBox>
        </div>
        </td>
        <td>
            <div class="form-group">
            <label for="title">TITLE:</label>
            <asp:TextBox ID="txttitle" runat="server" Text="" CssClass="form-control"></asp:TextBox>
        </div>
        </td>
        <td>
            <div class="form-group">
            <label for="title">DEPARTMENT:</label>
            <asp:TextBox ID="txtdepartment" runat="server" Text="" CssClass="form-control"></asp:TextBox>
        </div>
        </td>
        <td>
         <asp:Button ID="btnsearch" runat="server" OnClick="btnsearch_Click" Text="Search" CssClass="btn btn-default submit"/>

        </td>
        <td>
            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear This To View All"  CssClass="btn btn-default submit"/>

        </td>
    </tr>
</table>

