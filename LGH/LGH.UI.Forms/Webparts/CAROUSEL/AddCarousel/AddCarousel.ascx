<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="PublishingWebControls" Namespace="Microsoft.SharePoint.Publishing.WebControls" Assembly="Microsoft.SharePoint.Publishing, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddCarousel.ascx.cs" Inherits="LGH.UI.Forms.Webparts.CAROUSEL.AddCarousel.AddCarousel" %>


<script type="text/javascript">

    $(function () {
        $('ctl00_ctl36_g_c82441f6_6d8e_41da_bb16_5ba3ac17eb65_fileuploadThumbnailImage').val('thedata');
        $('input[type=file]').change(function () {

            var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;            if (regex.test($(this).val().toLowerCase())) {
                var reader = new FileReader();

                reader.onload = function (event) {
                    var imagePreview = $('#<%= imgPreview.ClientID%>'); //document.getElementById("imgPreview");
                    var imageFile = $('#<%= lblImageFilename.ClientID%>'); //document.getElementById("lblImageFilename");

                    imagePreview.attr("src", event.target.result);
                    imagePreview.css({ "display": "block" });

                    imageFile.html(document.querySelector('input[type=file]').files[0].name);
                    imageFile.css({ "display": "block" });

                    $('#<%= lblfileThumbnail.ClientID%>').html('');
                };

                reader.readAsDataURL(document.querySelector('input[type=file]').files[0]);
            }
            else {
                var imagePreview = $('#<%= imgPreview.ClientID%>'); //document.getElementById("imgPreview");
                var imageFile = $('#<%= lblImageFilename.ClientID%>'); //document.getElementById("lblImageFilename");

                imagePreview.attr("src", "");
                imagePreview.css({ "display": "none" });

                imageFile.html('');
                imageFile.css({ "display": "none" });

                $('#<%= lblfileThumbnail.ClientID%>').html('');

                alert("Please upload a valid image file.");
            }
        });

    });
</script>




<style type="text/css">
    .imgSpanPreviewClass img {
        border: 1px solid #ccc;
        width: 100px;
        height: 100px;
    }
</style>


<%-- <asp:UpdatePanel runat="server" ID="updPanel">
        <ContentTemplate>--%>
<div class="main_cont">
    <h2>Add Carousel</h2>
    <div class="col-md-7 add_form">
        <form role="form">
            <div class="form-group">
                <label for="title">Title:</label>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="lbltitle" CssClass="errorValidation" runat="server" Text=" " ForeColor="Red"></asp:Label>
            </div>
            <div class="form-group">
                <label for="description">Description:</label>
                <textarea class="form-control" rows="5" id="txtDescription" runat="server"></textarea>
                <asp:Label ID="lbldescription" CssClass="errorValidation" runat="server" Text=" " ForeColor="Red"></asp:Label>
            </div>
            <div class="form-group">
                <label for="image_tn">Image:</label>
                <asp:FileUpload ID="fileuploadThumbnailImage" runat="server" />
                <label>(655px X 450px)</label>
                <span class="imgSpanPreviewClass">
                    <img src="" style="display: none;" id="imgPreview" alt="Image Preview" runat="server" />
                </span>
                <span class="imgPreviewUrlClass">
                    <label id="lblImageFilename" style="display: none;" runat="server"></label>
                </span>
                <asp:Label ID="lblfileThumbnail" CssClass="errorValidation" runat="server" Text=" " ForeColor="Red"></asp:Label>
            </div>

            <div class="form-group urlPickerClass">
                <label for="title">Url:</label>
                <PublishingWebControls:AssetUrlSelector ID="urlSelector" IsUrlRequired="false" AllowExternalUrls="true" runat="server" 
                                            DefaultOpenLocationUrl="~site" DefaultToLastUsedLocation="true" UseImageAssetPicker="false"
                                            OverrideDialogTitle="Url Picker" OverrideDialogDescription="Select Page Url" />
                <asp:Label ID="lblurl" CssClass="errorValidation" runat="server" Text=" " ForeColor="Red"></asp:Label>
            </div>
            <div class="sub_but">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default submit" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default submit" OnClick="btnCancel_Click" />
            </div>
        </form>
    </div>
</div>
         <%--   </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>--%>


<asp:HiddenField ID="hdnImageUrl" runat="server" Value="" />