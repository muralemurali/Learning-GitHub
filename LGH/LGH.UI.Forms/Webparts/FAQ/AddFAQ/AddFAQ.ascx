﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddFAQ.ascx.cs" Inherits="LGH.UI.Forms.Webparts.FAQ.AddFAQ.AddFAQ" %>

				<div class="main_cont">
				<h2>Add FAQ</h2>
					<div class="col-md-8 add_form">
						<form role="form">
							<div class="form-group">
								<label for="title">Enter Question:</label>
								  <textarea class="form-control" rows="5" id="txtQuestion" runat="server"></textarea>
                                  <asp:Label ID="lblQuestion" CssClass="errorValidation" Text="" runat="server" ForeColor="Red"></asp:Label>
							</div>
							<div class="form-group">
								<label for="title">Enter Answer:</label>
								<textarea class="form-control" rows="5" id="txtAnswer" runat="server"></textarea>
                                <asp:Label ID="lblAnswer" CssClass="errorValidation" Text="" runat="server" ForeColor="Red"></asp:Label> 
							</div>
							<div class="sub_but">	
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default submit" OnClick="btnSave_Click"  />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default submit" OnClick="btnCancel_Click" />	
							</div> 
						</form>
					</div>
	</div>

