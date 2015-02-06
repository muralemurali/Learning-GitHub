using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using LGH.Model.Utility;
using LGH.Model.Entities;
using LGH.UI.Forms.Presenter;
using LGH.UI.Forms.Views;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;

namespace LGH.UI.Forms.Webparts.CAROUSEL.AddCarousel
{
    [ToolboxItemAttribute(false)]
    public partial class AddCarousel : WebPart,IAddCarouselHomeView
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public AddCarousel()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ChromeType = PartChromeType.None;
            InitializeControl();
        }


        #region Variable Declaration
        private AddCarouselHomePresenter _AddCarouselHomePresenter;
        #endregion

        #region Interface Implementation
        public event EventHandler AddCarouselItem;

        public string CarouselTitle
        {
            get { return txtTitle.Text; }
        }

        public string CarouselDescription
        {
            get { return txtDescription.InnerText; }
        }
        public string CarouselUrl
        {
            get { return urlSelector.AssetUrl; }
        }

        public FileUpload CarouselFileUploadThumbnailImage
        {

            //get { return hdnImageUrl.Value; }
            get { return fileuploadThumbnailImage; }
            //get { return fileuploadThumbnailImage.HasFile ? fileuploadThumbnailImage.PostedFile.FileName : hdnImageUrl.Value; }
        }
        #endregion


          protected void Page_Load(object sender, EventArgs e)
        {
            _AddCarouselHomePresenter = new AddCarouselHomePresenter(this);

           

        }

      
        protected void btnSave_Click(object sender, EventArgs e)
        {


            try
            {
                if (validation())
                {
                    if (AddCarouselItem != null)
                    {
                        AddCarouselItem(sender, e);
                        //   ClearControls();
                        //((TextBox)fileuploadThumbnailImage.Controls[0]).Text = "sometext";
                    }
                    HttpContext.Current.Response.Redirect("~/Pages/Carousel.aspx");

                }
              //  SuccessMessage.InnerText = "Items Added Successfully";

            }
            catch (NullReferenceException nullEx)
            {
                Logger.LogError("SSE WebParts", nullEx.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError("SSE WebParts", ex.Message);
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
            if (fileThumbnail == string.Empty)
            {
                //if (hdnImageUrl.Value == string.Empty)
                //{
                    returnValue = false;
                    lblfileThumbnail.Text = "Please upload the Image..!!";
                //}
            }

            if (fileThumbnail != string.Empty)
            {
                string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg" };
                string ext = Path.GetExtension(fileuploadThumbnailImage.PostedFile.FileName);
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
            txtTitle.Text = string.Empty;
            txtDescription.InnerText = string.Empty;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Redirect("~/Pages/Carousel.aspx");
        }
    }
}
