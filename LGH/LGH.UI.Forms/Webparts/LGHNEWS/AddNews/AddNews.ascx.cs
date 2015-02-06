using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using LGH.Model.Utility;
using LGH.Model.Entities;
using LGH.UI.Forms.Presenter.NEWSLINKS;
using LGH.UI.Forms.Views.NEWSLINKS;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web;
using System.Text.RegularExpressions;

namespace LGH.UI.Forms.Webparts.LGHNEWS.AddNews
{
    [ToolboxItemAttribute(false)]
    public partial class AddNews : WebPart,IAddNewsView
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public AddNews()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ChromeType = PartChromeType.None;
            InitializeControl();
        }

      

        #region Interface Implementation
        public event EventHandler AddNewsItem;

        public string shortNotes
        {
            get { return txtDescription.InnerText; }
        }

        public string LinkUrl
        {
            get { return txtTitle.Text +","+urlSelector.AssetUrl; }
        }
        #endregion

        #region variable Declaration

        private AddNewsPresenter _AddnewsPresenter;

        #endregion


        #region EventHandler

        protected void Page_Load(object sender, EventArgs e)
        {
            _AddnewsPresenter = new AddNewsPresenter(this);
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {


            try
            {
                if (validation())
                {
                    if (AddNewsItem != null)
                    {
                        AddNewsItem(sender, e);
                        //   ClearControls();
                    }
                    HttpContext.Current.Response.Redirect("~/Pages/News.aspx");

                }
                //  SuccessMessage.InnerText = "Items Added Successfully";

            }
            catch (NullReferenceException nullEx)
            {
                Logger.LogError("LGH WebParts", nullEx.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError("LGH WebParts", ex.Message);
            }
        }

        private bool validation()
        {
            var returnValue = true;

            lbldescription.Text = "";
            lbltitle.Text = "";

            lblurl.Text = "";
            Regex myRegex = new Regex(@"http(s)?://([\w-])+[\w-]+(/[\w- /?%&=]*)?", RegexOptions.Compiled);


            if (txtDescription.InnerText == string.Empty)
            {
                returnValue = false;
                lbldescription.Text = "Please Enter the Description..!!";
            }

            if (txtTitle.Text == string.Empty)
            {
                returnValue = false;
                lbltitle.Text = "Please Enter the Title";

            }
            if (urlSelector.AssetUrl == string.Empty)
            {
                returnValue = false;
                lblurl.Text = "Please Enter the URL";
            }
            else if (urlSelector.AssetUrl != string.Empty)
            {
                if (!urlSelector.AssetUrl.StartsWith("/"))
                {
                    if (!myRegex.IsMatch(urlSelector.AssetUrl))
                    {
                        returnValue = false;
                        lblurl.Text = "Please Enter the Valid URL";
                    }
                }
            }

            //if (txtUrl.Text == string.Empty)
            //{
            //    returnValue = false;
            //    lblurl.Text = "Please enter the URL";
            //}
            //else if (!myRegex.IsMatch(txtUrl.Text))
            //{
            //    returnValue = false;
            //    lblurl.Text = "Please Enter the valid URL";

            //}

            return returnValue;
        }
        private void ClearControls()
        {
            //txtUrl.Text = string.Empty;
            txtDescription.InnerText = string.Empty;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Redirect("~/Pages/News.aspx");
        }

        #endregion



    }
}
