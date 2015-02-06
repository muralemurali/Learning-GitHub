using Microsoft.SharePoint;
using LGH.Model.Utility;
using LGH.Model.Entities;
using LGH.UI.Forms.Presenter.NEWSLINKS;
using LGH.UI.Forms.Views.NEWSLINKS;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls.WebParts;
using System;
using System.ComponentModel;

namespace LGH.UI.Forms.Webparts.LGHNEWS.EditNews
{
    [ToolboxItemAttribute(false)]
    public partial class EditNews : WebPart,IEditNewsView
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public EditNews()
        {
        }
        #region Variable Declaration

        private EditNewsPresenter _editNewsPresenter;

        #endregion


        #region Interface Implementation
        public event EventHandler LoadData;

        public event EventHandler UpdateNewsItem;

        public IList<LGHNews> NewsInfoList
        {
            get;
            set;
        }

        public string QueryStringItemId
        {
            get { return hdnItemId.Value; }
        }

        public string LinkUrl
        {
            get { return txtTitle.Text + "," + urlSelector.AssetUrl; }
        }

        public string shortNotes
        {
            get { return txtDescription.InnerText; }
        }
        #endregion


        #region Event Handler
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ChromeType = PartChromeType.None;
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _editNewsPresenter = new EditNewsPresenter(this);

            try
            {
                if (Page.IsPostBack) return;

                if (Page.Request.QueryString["ItemId"] != null)
                {
                    hdnItemId.Value = Page.Request.QueryString["ItemId"].ToString();
                    // hdnItemId.Value = "2";
                    LoadData(sender, e);
                    BindDataIntoControls();
                }
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
            if (txtTitle.Text == string.Empty)
            {
                returnValue = false;
                lbltitle.Text = "Please Enter the Title";
            }

            //else if (!myRegex.IsMatch(txtUrl.Text))
            //{
            //    returnValue = false;
            //    lblurl.Text = "Please Enter the valid URL";

            //}

            //if (fileThumbnail == string.Empty)
            //{
            //    returnValue = false;
            //    lblfileThumbnail.Text = "Please upload the Image..!!";
            //}
            return returnValue;
        }

        private void BindDataIntoControls()
        {
            //Check if SponsorshipInfoList object has values or not, if not return
            if (NewsInfoList.Count <= 0) return;

            //If SponsorshipInfoList object has values, then bind the values into the control

            txtDescription.InnerText = NewsInfoList[0].shortNotes;

            string[] urlvalue = NewsInfoList[0].Url.Contains(",") ? NewsInfoList[0].Url.Split(',') : new string[] { "", "" };
            urlSelector.AssetUrl = urlvalue[0];
            txtTitle.Text = urlvalue[1];


        }

        /// <summary>
        /// Bind Items into Dropdown
        /// </summary>


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                if (validation())
                {
                    if (UpdateNewsItem != null)
                    {
                        UpdateNewsItem(sender, e);
                    }

                    //Page.Response.Write("<script type='text/javascript'>window.frameElement.commitPopup();</script>");
                    //Page.Response.Flush();
                    //Page.Response.End();
                    HttpContext.Current.Response.Redirect("~/Pages/News.aspx");
                }
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

        #endregion

    }
}
