using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using LGH.Model.Utility;
using LGH.Model.Entities;
using LGH.UI.Forms.Presenter;
using LGH.UI.Forms.Views;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;

namespace LGH.UI.Forms.Webparts.CAROUSEL.EditCarousel
{
    [ToolboxItemAttribute(false)]
    public partial class EditCarousel : WebPart,IEditCarouselView
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public EditCarousel()
        {
        }

        #region Variable Declaration

        private EditCarouselPresenter _EditCarouselPresenter;

        #endregion

        #region Interface Implementation
        public event EventHandler LoadData;

        public event EventHandler UpdateCarouselItem;

        public IList<CarouselHomePage> carouselInfoList
        {
            get;
            set;
        }

        public string QueryStringItemId
        {
            get { return hdnItemId.Value; }
        }

        public string carouselTitle
        {
            get { return txtTitle.Text; }
        }

        public string carouselDescription
        {
            get { return txtDescription.InnerText; }
        }

        public FileUpload ThumbnailImage
        {
            get { return fileuploadThumbnailImage; }
        }
        public string carouselUrl { get { return urlSelector.AssetUrl; } }
        public string FileUploadImageUrl
        {
            get { return hdnImageUrl.Value; }
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
            _EditCarouselPresenter = new EditCarouselPresenter(this);

            try
            {
                if (Page.IsPostBack) return;
                

                if (Page.Request.QueryString["ItemId"] != null)
                {
                    if (!Page.IsPostBack)
                    {
                        hdnItemId.Value = Page.Request.QueryString["ItemId"].ToString();
                        // hdnItemId.Value = "2";
                        LoadData(sender, e);
                        BindDataIntoControls();
                    }
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
            lbltitle.Text = "";
            lbldescription.Text = "";
            lblfileThumbnail.Text = "";
            lblurl.Text = "";
            Regex myRegex = new Regex(@"http(s)?://([\w-])+[\w-]+(/[\w- /?%&=]*)?", RegexOptions.Compiled);

            var fileThumbnail = fileuploadThumbnailImage.PostedFile.FileName;
            if (txtTitle.Text == string.Empty)
            {
                returnValue = false;
                lbltitle.Text = "Please Enter the Title..!!";
            }
            if (txtDescription.InnerText == string.Empty)
            {
                returnValue = false;
                lbldescription.Text = "Please Enter the Description..!!";
            }

            string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg" };
            string ext = Path.GetExtension(fileuploadThumbnailImage.PostedFile.FileName);

            if (string.IsNullOrEmpty(ext))
            {
                ext = Path.GetExtension(hdnImageUrl.Value);
            }
            bool isValidFile = false;
            for (int i = 0; i < validFileTypes.Length; i++)
            {
                if (ext == "." + validFileTypes[i])
                {
                    isValidFile = true;
                    break;
                }
            }

            if (!isValidFile)
            {
                returnValue = false;
                lblfileThumbnail.Text = "Invalid File. Please upload a File with extension " + string.Join(",", validFileTypes);
            }

            if (urlSelector.AssetUrl != string.Empty)
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

            //if (urlSelector.AssetUrl == string.Empty)
            //{
            //    returnValue = false;
            //    lblurl.Text = "Please Enter the Valid URL";
            //}
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
            if (carouselInfoList.Count <= 0) return;

            //If SponsorshipInfoList object has values, then bind the values into the control
            txtTitle.Text = carouselInfoList[0].CarouselTitle;
            txtDescription.InnerText = carouselInfoList[0].CarouselDescription;
            hdnImageUrl.Value = Regex.Match(carouselInfoList[0].CarouselImage, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
            string[] urlvalue = carouselInfoList[0].CarouselUrl.Contains(",") ? carouselInfoList[0].CarouselUrl.Split(',') : new string[] { "", "" };
            urlSelector.AssetUrl = urlvalue[0];
            imgPreview.Src = hdnImageUrl.Value;
            imgPreview.Style.Add("display", "block");

            lblImageFilename.InnerText = hdnImageUrl.Value;
            lblImageFilename.Style.Add("display", "block");

            lblfileThumbnail.Text = string.Empty;
            


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
                    if (UpdateCarouselItem != null)
                    {
                        UpdateCarouselItem(sender, e);
                    }

                    //Page.Response.Write("<script type='text/javascript'>window.frameElement.commitPopup();</script>");
                    //Page.Response.Flush();
                    //Page.Response.End();
                    HttpContext.Current.Response.Redirect("~/Pages/Carousel.aspx");
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
