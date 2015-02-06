<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SocialMediaTopNavigation.ascx.cs" Inherits="LGH.UI.HomePageTiles.Webparts.SocialMediaTopNavigation.SocialMediaTopNavigation" %>


<asp:HiddenField ID="hdnSearchUrl" runat="server" />


<div class="col-sm-3 navbar-header lpadding" id="SocialMediaTopNavigationDiv" runat="server">
</div>


<script src="../../_layouts/15/LGH.UI.HomePageTiles/jquery-1.9.1.js"></script>
<script src="../../_layouts/15/LGH.UI.HomePageTiles/jquery-ui1.10.3.js"></script>

<script type="text/javascript">

    function myFunction() {
        document.forms[0].onsubmit = function () {
            return false;
        }
        var url = $('#<%=hdnSearchUrl.ClientID%>').val();
        var value = $("#srch-term").val();
        if (value != "") {
            var encodeValue = $('<div/>').text(value).html();
            window.location.assign(url + "/Pages/Search.aspx?k=" + encodeValue);
        }
    }
</script>
