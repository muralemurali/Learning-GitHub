<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="PublishingWebControls" Namespace="Microsoft.SharePoint.Publishing.WebControls" Assembly="Microsoft.SharePoint.Publishing, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddNews.ascx.cs" Inherits="LGH.UI.Forms.Webparts.LGHNEWS.AddNews.AddNews" %>


<div class="main_cont">
    <h2>Add News</h2>
    <div class="col-md-7 add_form">
        <form role="form">
            <div class="form-group">
                <label for="title">Title:</label>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="lbltitle" CssClass="errorValidation" runat="server" Text=" " ForeColor="Red"></asp:Label>
            </div>
            <div class="form-group urlPickerClass">
                <label for="title">Url:</label>
                <PublishingWebControls:AssetUrlSelector ID="urlSelector" IsUrlRequired="false" AllowExternalUrls="true" runat="server"
                    DefaultOpenLocationUrl="~site" DefaultToLastUsedLocation="true" UseImageAssetPicker="false"
                    OverrideDialogTitle="Url Picker" OverrideDialogDescription="Select Page Url" />
              <p> <asp:Label ID="lblurl" CssClass="errorValidation" runat="server" Text=" " ForeColor="Red"></asp:Label></p> 
            </div>

            <div class="form-group">
                <label for="description">Description:</label>
                <textarea class="form-control" rows="5" id="txtDescription" runat="server"></textarea>
                <asp:Label ID="lbldescription" CssClass="errorValidation" runat="server" Text=" " ForeColor="Red"></asp:Label>
            </div>


            <div class="sub_but">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default submit" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default submit" OnClick="btnCancel_Click" />
            </div>
        </form>
    </div>
</div>
