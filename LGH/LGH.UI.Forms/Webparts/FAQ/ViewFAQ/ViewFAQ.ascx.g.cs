﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LGH.UI.Forms.Webparts.FAQ.ViewFAQ {
    using System.Web.UI.WebControls.Expressions;
    using System.Web.UI.HtmlControls;
    using System.Collections;
    using System.Text;
    using System.Web.UI;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Microsoft.SharePoint.WebPartPages;
    using System.Web.SessionState;
    using System.Configuration;
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
    using System.Collections.Specialized;
    using System.Web.UI.WebControls.WebParts;
    using Microsoft.SharePoint.WebControls;
    
    
    public partial class ViewFAQ {
        
        protected global::System.Web.UI.HtmlControls.HtmlGenericControl spanFaq;
        
        protected global::System.Web.UI.WebControls.GridView gridFAQ;
        
        protected global::System.Web.UI.UpdatePanel updPanel;
        
        protected global::System.Web.UI.WebControls.HiddenField hdnitemid;
        
        public static implicit operator global::System.Web.UI.TemplateControl(ViewFAQ target) 
        {
            return target == null ? null : target.TemplateControl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.HtmlControls.HtmlGenericControl @__BuildControlspanFaq() {
            global::System.Web.UI.HtmlControls.HtmlGenericControl @__ctrl;
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlGenericControl("span");
            this.spanFaq = @__ctrl;
            @__ctrl.TemplateControl = this;
            @__ctrl.ID = "spanFaq";
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("class", "addnew glyphicon glyphicon-plus-sign pull-right");
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("<a href=\"/Pages/AddFaq.aspx\" class=\"a_new\">Add New</a>"));
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.BoundField @__BuildControl__control4() {
            global::System.Web.UI.WebControls.BoundField @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.BoundField();
            @__ctrl.HeaderText = "ID";
            @__ctrl.DataField = "ItemId";
            @__ctrl.HeaderStyle.CssClass = "GridHeaderStyle";
            @__ctrl.ItemStyle.CssClass = "GridItemStyle";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Label @__BuildControl__control7() {
            global::System.Web.UI.WebControls.Label @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Label();
            @__ctrl.TemplateControl = this;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lblTitle";
            @__ctrl.DataBinding += new System.EventHandler(this.@__DataBinding__control7);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public void @__DataBinding__control7(object sender, System.EventArgs e) {
            System.Web.UI.WebControls.Label dataBindingExpressionBuilderTarget;
            System.Web.UI.IDataItemContainer Container;
            dataBindingExpressionBuilderTarget = ((System.Web.UI.WebControls.Label)(sender));
            Container = ((System.Web.UI.IDataItemContainer)(dataBindingExpressionBuilderTarget.BindingContainer));
            dataBindingExpressionBuilderTarget.Text = global::System.Convert.ToString( Eval("Question") , global::System.Globalization.CultureInfo.CurrentCulture);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControl__control6(System.Web.UI.Control @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                                        "));
            global::System.Web.UI.WebControls.Label @__ctrl1;
            @__ctrl1 = this.@__BuildControl__control7();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                                    "));
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.TemplateField @__BuildControl__control5() {
            global::System.Web.UI.WebControls.TemplateField @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.TemplateField();
            @__ctrl.ItemTemplate = new System.Web.UI.CompiledBindableTemplateBuilder(new System.Web.UI.BuildTemplateMethod(this.@__BuildControl__control6), null);
            @__ctrl.HeaderText = "Question";
            @__ctrl.HeaderStyle.Font.Bold = true;
            @__ctrl.HeaderStyle.CssClass = "gridSponsorshipStyle thead th";
            @__ctrl.ItemStyle.Width = new System.Web.UI.WebControls.Unit(45D, global::System.Web.UI.WebControls.UnitType.Percentage);
            @__ctrl.ItemStyle.HorizontalAlign = global::System.Web.UI.WebControls.HorizontalAlign.Left;
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Label @__BuildControl__control10() {
            global::System.Web.UI.WebControls.Label @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Label();
            @__ctrl.TemplateControl = this;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lblDescription";
            @__ctrl.DataBinding += new System.EventHandler(this.@__DataBinding__control10);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public void @__DataBinding__control10(object sender, System.EventArgs e) {
            System.Web.UI.WebControls.Label dataBindingExpressionBuilderTarget;
            System.Web.UI.IDataItemContainer Container;
            dataBindingExpressionBuilderTarget = ((System.Web.UI.WebControls.Label)(sender));
            Container = ((System.Web.UI.IDataItemContainer)(dataBindingExpressionBuilderTarget.BindingContainer));
            dataBindingExpressionBuilderTarget.Text = global::System.Convert.ToString( Eval("Answer") , global::System.Globalization.CultureInfo.CurrentCulture);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControl__control9(System.Web.UI.Control @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                                        "));
            global::System.Web.UI.WebControls.Label @__ctrl1;
            @__ctrl1 = this.@__BuildControl__control10();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                                    "));
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.TemplateField @__BuildControl__control8() {
            global::System.Web.UI.WebControls.TemplateField @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.TemplateField();
            @__ctrl.ItemTemplate = new System.Web.UI.CompiledBindableTemplateBuilder(new System.Web.UI.BuildTemplateMethod(this.@__BuildControl__control9), null);
            @__ctrl.HeaderText = "Answer";
            @__ctrl.HeaderStyle.Font.Bold = true;
            @__ctrl.ItemStyle.Width = new System.Web.UI.WebControls.Unit(45D, global::System.Web.UI.WebControls.UnitType.Percentage);
            @__ctrl.ItemStyle.HorizontalAlign = global::System.Web.UI.WebControls.HorizontalAlign.Left;
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.LinkButton @__BuildControl__control13() {
            global::System.Web.UI.WebControls.LinkButton @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.LinkButton();
            @__ctrl.TemplateControl = this;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lnkEdit";
            @__ctrl.CssClass = "editItem";
            @__ctrl.Text = "";
            @__ctrl.ToolTip = "Edit This Item";
            @__ctrl.DataBinding += new System.EventHandler(this.@__DataBinding__control13);
            @__ctrl.Click -= new System.EventHandler(this.lnkEdit_Click);
            @__ctrl.Click += new System.EventHandler(this.lnkEdit_Click);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public void @__DataBinding__control13(object sender, System.EventArgs e) {
            System.Web.UI.WebControls.LinkButton dataBindingExpressionBuilderTarget;
            System.Web.UI.IDataItemContainer Container;
            dataBindingExpressionBuilderTarget = ((System.Web.UI.WebControls.LinkButton)(sender));
            Container = ((System.Web.UI.IDataItemContainer)(dataBindingExpressionBuilderTarget.BindingContainer));
            dataBindingExpressionBuilderTarget.CommandArgument = global::System.Convert.ToString(Eval("ItemId"), global::System.Globalization.CultureInfo.CurrentCulture);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.LinkButton @__BuildControl__control14() {
            global::System.Web.UI.WebControls.LinkButton @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.LinkButton();
            @__ctrl.TemplateControl = this;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lnkDelete";
            @__ctrl.CssClass = "deleteItem";
            @__ctrl.Text = "";
            @__ctrl.ToolTip = "Delete This Item";
            @__ctrl.OnClientClick = "return fnConfirm();";
            @__ctrl.DataBinding += new System.EventHandler(this.@__DataBinding__control14);
            @__ctrl.Click -= new System.EventHandler(this.lnkDelete_Click);
            @__ctrl.Click += new System.EventHandler(this.lnkDelete_Click);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public void @__DataBinding__control14(object sender, System.EventArgs e) {
            System.Web.UI.WebControls.LinkButton dataBindingExpressionBuilderTarget;
            System.Web.UI.IDataItemContainer Container;
            dataBindingExpressionBuilderTarget = ((System.Web.UI.WebControls.LinkButton)(sender));
            Container = ((System.Web.UI.IDataItemContainer)(dataBindingExpressionBuilderTarget.BindingContainer));
            dataBindingExpressionBuilderTarget.CommandArgument = global::System.Convert.ToString(Eval("ItemId"), global::System.Globalization.CultureInfo.CurrentCulture);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControl__control12(System.Web.UI.Control @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                                        "));
            global::System.Web.UI.WebControls.LinkButton @__ctrl1;
            @__ctrl1 = this.@__BuildControl__control13();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                                        "));
            global::System.Web.UI.WebControls.LinkButton @__ctrl2;
            @__ctrl2 = this.@__BuildControl__control14();
            @__parser.AddParsedSubObject(@__ctrl2);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                                    "));
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.TemplateField @__BuildControl__control11() {
            global::System.Web.UI.WebControls.TemplateField @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.TemplateField();
            @__ctrl.ItemTemplate = new System.Web.UI.CompiledBindableTemplateBuilder(new System.Web.UI.BuildTemplateMethod(this.@__BuildControl__control12), null);
            @__ctrl.HeaderStyle.Width = new System.Web.UI.WebControls.Unit(50D, global::System.Web.UI.WebControls.UnitType.Pixel);
            @__ctrl.HeaderStyle.Font.Bold = true;
            @__ctrl.HeaderText = "Actions";
            @__ctrl.ItemStyle.Width = new System.Web.UI.WebControls.Unit(10D, global::System.Web.UI.WebControls.UnitType.Percentage);
            @__ctrl.ItemStyle.HorizontalAlign = global::System.Web.UI.WebControls.HorizontalAlign.Center;
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControl__control3(System.Web.UI.WebControls.DataControlFieldCollection @__ctrl) {
            global::System.Web.UI.WebControls.BoundField @__ctrl1;
            @__ctrl1 = this.@__BuildControl__control4();
            @__ctrl.Add(@__ctrl1);
            global::System.Web.UI.WebControls.TemplateField @__ctrl2;
            @__ctrl2 = this.@__BuildControl__control5();
            @__ctrl.Add(@__ctrl2);
            global::System.Web.UI.WebControls.TemplateField @__ctrl3;
            @__ctrl3 = this.@__BuildControl__control8();
            @__ctrl.Add(@__ctrl3);
            global::System.Web.UI.WebControls.TemplateField @__ctrl4;
            @__ctrl4 = this.@__BuildControl__control11();
            @__ctrl.Add(@__ctrl4);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControl__control15(System.Web.UI.WebControls.TableItemStyle @__ctrl) {
            @__ctrl.CssClass = "sseGridStylesRow";
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.GridView @__BuildControlgridFAQ() {
            global::System.Web.UI.WebControls.GridView @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.GridView();
            this.gridFAQ = @__ctrl;
            @__ctrl.TemplateControl = this;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "gridFAQ";
            @__ctrl.Width = new System.Web.UI.WebControls.Unit(100D, global::System.Web.UI.WebControls.UnitType.Percentage);
            @__ctrl.AutoGenerateColumns = false;
            @__ctrl.GridLines = global::System.Web.UI.WebControls.GridLines.None;
            @__ctrl.CellSpacing = 2;
            @__ctrl.CssClass = "sseGridStyles";
            @__ctrl.HorizontalAlign = global::System.Web.UI.WebControls.HorizontalAlign.Center;
            this.@__BuildControl__control3(@__ctrl.Columns);
            this.@__BuildControl__control15(@__ctrl.RowStyle);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControl__control2(System.Web.UI.Control @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"
             
                    <div class=""breadcrumbs"">
				        <ul class=""breadcrumb_list"">
					        <li><a href=""/Pages/LGHome.aspx"">Home</a><span>></span></li>
					        <li>FAQ's</li>
				        </ul>
			        </div>
                    <div class=""main_content"">		
                        <h2>
            global::System.Web.UI.HtmlControls.HtmlGenericControl @__ctrl1;
            @__ctrl1 = this.@__BuildControlspanFaq();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                        </h2>\r\n                        "));
            global::System.Web.UI.WebControls.GridView @__ctrl2;
            @__ctrl2 = this.@__BuildControlgridFAQ();
            @__parser.AddParsedSubObject(@__ctrl2);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(" \r\n                    </div>\r\n             \r\n        "));
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.UpdatePanel @__BuildControlupdPanel() {
            global::System.Web.UI.UpdatePanel @__ctrl;
            @__ctrl = new global::System.Web.UI.UpdatePanel();
            this.updPanel = @__ctrl;
            @__ctrl.ContentTemplate = new System.Web.UI.CompiledTemplateBuilder(new System.Web.UI.BuildTemplateMethod(this.@__BuildControl__control2));
            @__ctrl.ID = "updPanel";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.HiddenField @__BuildControlhdnitemid() {
            global::System.Web.UI.WebControls.HiddenField @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.HiddenField();
            this.hdnitemid = @__ctrl;
            @__ctrl.ID = "hdnitemid";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControlTree(global::LGH.UI.Forms.Webparts.FAQ.ViewFAQ.ViewFAQ @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n\r\n\r\n<style type=\"text/css\">\r\n    .GridHeaderStyle\r\n    {\r\n\t    display: none;\r\n" +
                        "    }\r\n    .GridItemStyle\r\n    {\r\n\t    display: none;\r\n    }\r\n</style>\r\n\r\n\r\n\r\n<d" +
                        "iv>\r\n    "));
            global::System.Web.UI.UpdatePanel @__ctrl1;
            @__ctrl1 = this.@__BuildControlupdPanel();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n</div>\r\n\r\n<script type=\"text/javascript\">\r\n    function fnConfirm() {\r\n        " +
                        "if (confirm(\"Do you want to delete the item?\") == true)\r\n            return true" +
                        ";\r\n        else\r\n            return false;\r\n    }\r\n</script>\r\n\r\n"));
            global::System.Web.UI.WebControls.HiddenField @__ctrl2;
            @__ctrl2 = this.@__BuildControlhdnitemid();
            @__parser.AddParsedSubObject(@__ctrl2);
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