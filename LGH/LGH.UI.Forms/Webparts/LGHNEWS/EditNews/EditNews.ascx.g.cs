﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LGH.UI.Forms.Webparts.LGHNEWS.EditNews {
    using System.Web.UI.WebControls.Expressions;
    using System.Collections;
    using System.Text;
    using System.Web.UI;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Microsoft.SharePoint.WebControls;
    using Microsoft.SharePoint.Publishing.WebControls;
    using System.Web.SessionState;
    using Microsoft.SharePoint.WebPartPages;
    using Microsoft.SharePoint;
    using System.Web;
    using System.Web.DynamicData;
    using System.Web.Caching;
    using System.Web.Profile;
    using System.ComponentModel.DataAnnotations;
    using System.Web.UI.WebControls;
    using System.Web.Security;
    using System;
    using Microsoft.SharePoint.Utilities;
    using System.Text.RegularExpressions;
    using System.Configuration;
    using System.Web.UI.WebControls.WebParts;
    using System.Collections.Specialized;
    using System.Web.UI.HtmlControls;
    
    
    public partial class EditNews {
        
        protected global::System.Web.UI.WebControls.TextBox txtTitle;
        
        protected global::System.Web.UI.WebControls.Label lbltitle;
        
        protected global::Microsoft.SharePoint.Publishing.WebControls.AssetUrlSelector urlSelector;
        
        protected global::System.Web.UI.WebControls.Label lblurl;
        
        protected global::System.Web.UI.HtmlControls.HtmlTextArea txtDescription;
        
        protected global::System.Web.UI.WebControls.Label lbldescription;
        
        protected global::System.Web.UI.WebControls.Button btnUpdate;
        
        protected global::System.Web.UI.UpdatePanel updPanel;
        
        protected global::System.Web.UI.WebControls.HiddenField hdnItemId;
        
        protected global::System.Web.UI.WebControls.HiddenField hdnImageUrl;
        
        public static implicit operator global::System.Web.UI.TemplateControl(EditNews target) 
        {
            return target == null ? null : target.TemplateControl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.TextBox @__BuildControltxtTitle() {
            global::System.Web.UI.WebControls.TextBox @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.TextBox();
            this.txtTitle = @__ctrl;
            @__ctrl.TemplateControl = this;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "txtTitle";
            @__ctrl.CssClass = "form-control";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Label @__BuildControllbltitle() {
            global::System.Web.UI.WebControls.Label @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Label();
            this.lbltitle = @__ctrl;
            @__ctrl.TemplateControl = this;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lbltitle";
            @__ctrl.CssClass = "errorValidation";
            @__ctrl.Text = " ";
            @__ctrl.ForeColor = global::System.Drawing.Color.Red;
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::Microsoft.SharePoint.Publishing.WebControls.AssetUrlSelector @__BuildControlurlSelector() {
            global::Microsoft.SharePoint.Publishing.WebControls.AssetUrlSelector @__ctrl;
            @__ctrl = new global::Microsoft.SharePoint.Publishing.WebControls.AssetUrlSelector();
            this.urlSelector = @__ctrl;
            @__ctrl.TemplateControl = this;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "urlSelector";
            @__ctrl.IsUrlRequired = false;
            @__ctrl.AllowExternalUrls = true;
            @__ctrl.DefaultOpenLocationUrl = "~site";
            @__ctrl.DefaultToLastUsedLocation = true;
            @__ctrl.UseImageAssetPicker = false;
            @__ctrl.OverrideDialogTitle = "Url Picker";
            @__ctrl.OverrideDialogDescription = "Select Page Url";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Label @__BuildControllblurl() {
            global::System.Web.UI.WebControls.Label @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Label();
            this.lblurl = @__ctrl;
            @__ctrl.TemplateControl = this;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lblurl";
            @__ctrl.CssClass = "errorValidation";
            @__ctrl.Text = " ";
            @__ctrl.ForeColor = global::System.Drawing.Color.Red;
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.HtmlControls.HtmlTextArea @__BuildControltxtDescription() {
            global::System.Web.UI.HtmlControls.HtmlTextArea @__ctrl;
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlTextArea();
            this.txtDescription = @__ctrl;
            @__ctrl.TemplateControl = this;
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("class", "form-control");
            @__ctrl.Rows = 5;
            @__ctrl.ID = "txtDescription";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Label @__BuildControllbldescription() {
            global::System.Web.UI.WebControls.Label @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Label();
            this.lbldescription = @__ctrl;
            @__ctrl.TemplateControl = this;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lbldescription";
            @__ctrl.CssClass = "errorValidation";
            @__ctrl.Text = " ";
            @__ctrl.ForeColor = global::System.Drawing.Color.Red;
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Button @__BuildControlbtnUpdate() {
            global::System.Web.UI.WebControls.Button @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Button();
            this.btnUpdate = @__ctrl;
            @__ctrl.TemplateControl = this;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "btnUpdate";
            @__ctrl.Text = "Update";
            @__ctrl.CssClass = "btn btn-default submit";
            @__ctrl.Click -= new System.EventHandler(this.btnUpdate_Click);
            @__ctrl.Click += new System.EventHandler(this.btnUpdate_Click);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControl__control2(System.Web.UI.Control @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"
            <div class=""main_cont"">
                <h2>Edit News</h2>
                <div class=""col-md-7 add_form"">
                    <form role=""form"">
                        <div class=""form-group"">
                            <label for=""title"">Title:</label>
                            "));
            global::System.Web.UI.WebControls.TextBox @__ctrl1;
            @__ctrl1 = this.@__BuildControltxtTitle();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                            "));
            global::System.Web.UI.WebControls.Label @__ctrl2;
            @__ctrl2 = this.@__BuildControllbltitle();
            @__parser.AddParsedSubObject(@__ctrl2);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                        </div>\r\n\r\n                        <div class=\"form-grou" +
                        "p urlPickerClass\">\r\n                            <label for=\"title\">Url:</label>\r" +
                        "\n                            "));
            global::Microsoft.SharePoint.Publishing.WebControls.AssetUrlSelector @__ctrl3;
            @__ctrl3 = this.@__BuildControlurlSelector();
            @__parser.AddParsedSubObject(@__ctrl3);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                            <p>"));
            global::System.Web.UI.WebControls.Label @__ctrl4;
            @__ctrl4 = this.@__BuildControllblurl();
            @__parser.AddParsedSubObject(@__ctrl4);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("</p>\r\n                        </div>\r\n                        <div class=\"form-gr" +
                        "oup\">\r\n                            <label for=\"description\">Description:</label>" +
                        "\r\n                            "));
            global::System.Web.UI.HtmlControls.HtmlTextArea @__ctrl5;
            @__ctrl5 = this.@__BuildControltxtDescription();
            @__parser.AddParsedSubObject(@__ctrl5);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                            "));
            global::System.Web.UI.WebControls.Label @__ctrl6;
            @__ctrl6 = this.@__BuildControllbldescription();
            @__parser.AddParsedSubObject(@__ctrl6);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                        </div>\r\n\r\n\r\n\r\n                        <div class=\"sub_b" +
                        "ut\">\r\n                            "));
            global::System.Web.UI.WebControls.Button @__ctrl7;
            @__ctrl7 = this.@__BuildControlbtnUpdate();
            @__parser.AddParsedSubObject(@__ctrl7);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n\r\n                        </div>\r\n                    </form>\r\n                " +
                        "</div>\r\n            </div>\r\n        "));
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.PostBackTrigger @__BuildControl__control4() {
            global::System.Web.UI.PostBackTrigger @__ctrl;
            @__ctrl = new global::System.Web.UI.PostBackTrigger();
            @__ctrl.ControlID = "btnUpdate";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControl__control3(System.Web.UI.UpdatePanelTriggerCollection @__ctrl) {
            global::System.Web.UI.PostBackTrigger @__ctrl1;
            @__ctrl1 = this.@__BuildControl__control4();
            @__ctrl.Add(@__ctrl1);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.UpdatePanel @__BuildControlupdPanel() {
            global::System.Web.UI.UpdatePanel @__ctrl;
            @__ctrl = new global::System.Web.UI.UpdatePanel();
            this.updPanel = @__ctrl;
            @__ctrl.ContentTemplate = new System.Web.UI.CompiledTemplateBuilder(new System.Web.UI.BuildTemplateMethod(this.@__BuildControl__control2));
            @__ctrl.ID = "updPanel";
            this.@__BuildControl__control3(@__ctrl.Triggers);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.HiddenField @__BuildControlhdnItemId() {
            global::System.Web.UI.WebControls.HiddenField @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.HiddenField();
            this.hdnItemId = @__ctrl;
            @__ctrl.ID = "hdnItemId";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.HiddenField @__BuildControlhdnImageUrl() {
            global::System.Web.UI.WebControls.HiddenField @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.HiddenField();
            this.hdnImageUrl = @__ctrl;
            @__ctrl.ID = "hdnImageUrl";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControlTree(global::LGH.UI.Forms.Webparts.LGHNEWS.EditNews.EditNews @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n\r\n\r\n\r\n<div>\r\n    "));
            global::System.Web.UI.UpdatePanel @__ctrl1;
            @__ctrl1 = this.@__BuildControlupdPanel();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n</div>\r\n\r\n<div>\r\n    "));
            global::System.Web.UI.WebControls.HiddenField @__ctrl2;
            @__ctrl2 = this.@__BuildControlhdnItemId();
            @__parser.AddParsedSubObject(@__ctrl2);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n    "));
            global::System.Web.UI.WebControls.HiddenField @__ctrl3;
            @__ctrl3 = this.@__BuildControlhdnImageUrl();
            @__parser.AddParsedSubObject(@__ctrl3);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n</div>\r\n"));
        }
        
        private void InitializeControl() {
            this.@__BuildControlTree(this);
            this.Load += new global::System.EventHandler(this.Page_Load);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        protected virtual object Eval(string expression) {
            return global::System.Web.UI.DataBinder.Eval(this.Page.GetDataItem(), expression);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        protected virtual string Eval(string expression, string format) {
            return global::System.Web.UI.DataBinder.Eval(this.Page.GetDataItem(), expression, format);
        }
    }
}