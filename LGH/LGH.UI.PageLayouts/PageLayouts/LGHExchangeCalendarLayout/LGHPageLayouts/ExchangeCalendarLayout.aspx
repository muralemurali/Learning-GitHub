﻿<%@ Page language="C#" Inherits="Microsoft.SharePoint.Publishing.PublishingLayoutPage,Microsoft.SharePoint.Publishing,Version=15.0.0.0,Culture=neutral,PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePointWebControls" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
 <%@ Register Tagprefix="PublishingWebControls" Namespace="Microsoft.SharePoint.Publishing.WebControls" Assembly="Microsoft.SharePoint.Publishing, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
 <%@ Register Tagprefix="PublishingNavigation" Namespace="Microsoft.SharePoint.Publishing.Navigation" Assembly="Microsoft.SharePoint.Publishing, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Content ContentPlaceholderID="PlaceHolderAdditionalPageHead" runat="server">
	<SharePointWebControls:CssRegistration ID="CssRegistration1" name="<% $SPUrl:~sitecollection/Style Library/~language/Themable/Core Styles/pagelayouts15.css %>" runat="server"/>
	<PublishingWebControls:EditModePanel ID="EditModePanel1" runat="server">
		<!-- Styles for edit mode only-->
		<SharePointWebControls:CssRegistration ID="CssRegistration2" name="<% $SPUrl:~sitecollection/Style Library/~language/Themable/Core Styles/editmode15.css %>"
			After="<% $SPUrl:~sitecollection/Style Library/~language/Themable/Core Styles/pagelayouts15.css %>" runat="server"/>
	</PublishingWebControls:EditModePanel>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderPageTitle" runat="server">
	<SharePointWebControls:ListProperty ID="ListProperty1" Property="Title" runat="server"/> - 
	<SharePointWebControls:FieldValue ID="FieldValue1" FieldName="Title" runat="server"/>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderPageTitleInTitleArea" runat="server">
	<SharePointWebControls:FieldValue ID="FieldValue2" FieldName="Title" runat="server"/>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderTitleBreadcrumb" runat="server"> 
	<SharePointWebControls:ListSiteMapPath ID="ListSiteMapPath1" runat="server" SiteMapProviders="CurrentNavigationSwitchableProvider" RenderCurrentNodeAsLink="false" PathSeparator="" CssClass="s4-breadcrumb" NodeStyle-CssClass="s4-breadcrumbNode" CurrentNodeStyle-CssClass="s4-breadcrumbCurrentNode" RootNodeStyle-CssClass="s4-breadcrumbRootNode" NodeImageOffsetX=0 NodeImageOffsetY=289 NodeImageWidth=16 NodeImageHeight=16 NodeImageUrl="/_layouts/15/images/fgimg.png?rev=23" HideInteriorRootNodes="true" SkipLinkText=""/> </asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderPageDescription" runat="server">
	<SharePointWebControls:ProjectProperty ID="ProjectProperty1" Property="Description" runat="server"/>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderBodyRightMargin" runat="server">
	<div height=100% class="ms-pagemargin"><IMG SRC="/_layouts/images/blank.gif" width=10 height=1 alt=""></div>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderMain" runat="server">
	<div class="welcome blank-wp">
		<PublishingWebControls:EditModePanel ID="EditModePanel2" runat="server" CssClass="edit-mode-panel title-edit">
			<SharePointWebControls:TextField ID="TextField1" runat="server" FieldName="Title"/>
		</PublishingWebControls:EditModePanel>
		<div class="welcome-content">
			<PublishingWebControls:RichHtmlField ID="RichHtmlField1" FieldName="PublishingPageContent" HasInitialFocus="True" MinimumEditHeight="400px" runat="server"/>
		</div>
		<div class="ms-table ms-fullWidth">
			<div class="tableCol-75">
				<div class="cell-margin">
					<WebPartPages:WebPartZone runat="server" Title="<%$Resources:cms,WebPartZoneTitle_Header%>" ID="Header"><ZoneTemplate></ZoneTemplate></WebPartPages:WebPartZone>
				</div>
				<div class="ms-table ms-fullWidth">
					<div class="cell-margin tableCol-50">
						<WebPartPages:WebPartZone runat="server" Title="<%$Resources:cms,WebPartZoneTitle_TopLeft%>" ID="TopLeftRow"><ZoneTemplate></ZoneTemplate></WebPartPages:WebPartZone>
					</div>
					<div class="cell-margin tableCol-50">
						<WebPartPages:WebPartZone runat="server" Title="<%$Resources:cms,WebPartZoneTitle_TopRight%>" ID="TopRightRow"><ZoneTemplate></ZoneTemplate></WebPartPages:WebPartZone>
					</div>
				</div>
				<div class="ms-table ms-fullWidth">
					<div class="cell-margin tableCol-33">
						<WebPartPages:WebPartZone runat="server" Title="<%$Resources:cms,WebPartZoneTitle_CenterLeft%>" ID="CenterLeftColumn"><ZoneTemplate></ZoneTemplate></WebPartPages:WebPartZone>
					</div>
					<div class="cell-margin tableCol-33">
						<WebPartPages:WebPartZone runat="server" Title="<%$Resources:cms,WebPartZoneTitle_Center%>" ID="CenterColumn"><ZoneTemplate></ZoneTemplate></WebPartPages:WebPartZone>
					</div>
					<div class="cell-margin tableCol-33">
						<WebPartPages:WebPartZone runat="server" Title="<%$Resources:cms,WebPartZoneTitle_CenterRight%>" ID="CenterRightColumn"><ZoneTemplate></ZoneTemplate></WebPartPages:WebPartZone>
					</div>
				</div>
				<div class="cell-margin">
					<WebPartPages:WebPartZone runat="server" Title="<%$Resources:cms,WebPartZoneTitle_Footer%>" ID="Footer"><ZoneTemplate></ZoneTemplate></WebPartPages:WebPartZone>
				</div>
			</div>
			<div class="cell-margin tableCol-25">
			<WebPartPages:WebPartZone runat="server" Title="<%$Resources:cms,WebPartZoneTitle_Right%>" ID="RightColumn" Orientation="Vertical"><ZoneTemplate></ZoneTemplate></WebPartPages:WebPartZone>
			</div>
			<SharePointWebControls:ScriptBlock ID="ScriptBlock1" runat="server">
			if(typeof(MSOLayout_MakeInvisibleIfEmpty) == &quot;function&quot;) 
			{MSOLayout_MakeInvisibleIfEmpty();}</SharePointWebControls:ScriptBlock>
		</div>
	</div>
</asp:Content>
