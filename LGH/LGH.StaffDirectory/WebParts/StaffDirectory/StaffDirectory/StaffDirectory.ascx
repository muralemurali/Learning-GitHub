<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StaffDirectory.ascx.cs" Inherits="LGH.StaffDirectory.StaffDirectory.StaffDirectory.StaffDirectory" %>


<div class="col-md-9 lpadding rpadding">
    <div class="main_cont">
        <h2>Staff Directory</h2>
        <div class="col-md-7 add_form">
            <form role="form">
                <div class="form-group">
                    <label for="title">Name:</label>
                    <asp:TextBox ID="txtName1" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Label ID="lbltitle" CssClass="errorValidation" runat="server" Text=" " ForeColor="Red"></asp:Label>
                </div>
                <div class="form-group">
                    <label for="description">Department:</label>
                    <asp:TextBox ID="txtDepartment1" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Label ID="lbldescription" CssClass="errorValidation" runat="server" Text=" " ForeColor="Red"></asp:Label>
                </div>

                <div class="form-group">
                    <label for="title">Title:</label>
                    <asp:TextBox ID="txtTitle1" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Label ID="lblurl" CssClass="errorValidation" runat="server" Text=" " ForeColor="Red"></asp:Label>
                </div>
                <div class="sub_but">
                    <asp:Button ID="btnSearch1" runat="server" Text="Search" CssClass="btn btn-default submit" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnClear1" runat="server" Text="Clear" CssClass="btn btn-default submit" OnClick="btnClear_Click" />
                </div>
            </form>
        </div>

        <SharePoint:SPGridView runat="server" ID="_gridView" AutoGenerateColumns="false"  CssClass="sseGridStyles"  ForeColor="#333333"  Width="100%" >

        

        <AlternatingRowStyle BackColor="White" />
        <EditRowStyle BackColor="#7C6F57" />

        <RowStyle BackColor="#E3EAEB" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F8FAFA" />
        <SortedAscendingHeaderStyle BackColor="#246B61" />
        <SortedDescendingCellStyle BackColor="#D4DFE1" />
        <SortedDescendingHeaderStyle BackColor="#15524A" />
        <PagerStyle HorizontalAlign="Center" ForeColor="BlueViolet" />


    </SharePoint:SPGridView>
    </div>
</div>




<div>

    </div>