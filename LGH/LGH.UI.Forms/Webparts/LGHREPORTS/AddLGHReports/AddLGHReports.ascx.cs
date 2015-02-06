using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using LGH.Model.Utility;
using LGH.Model.Entities;
using LGH.UI.Forms.Presenter;
using LGH.UI.Forms.Views.REPORTSLINKS;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web;
using System.Text.RegularExpressions;


namespace LGH.UI.Forms.Webparts.LGHREPORTS.AddLGHReports
{
    [ToolboxItemAttribute(false)]
    public partial class AddLGHReports : WebPart,IAddReportView
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public AddLGHReports()
        {
        }

       
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ChromeType = PartChromeType.None;
            InitializeControl();
        }


        #region Variable Declaration
        private AddReportPresenter _AddReportsPresenter;
        #endregion

        #region Interface Implementation
        public event EventHandler AddReportsItem;
       
        public string shortNotes
        {
            get { return txtDescription.InnerText; }
        }

        public string LinkUrl
        {
            get { return txtTitle.Text + "," + urlSelector.AssetUrl; }
        }

        #endregion


          protected void Page_Load(object sender, EventArgs e)
        {
            _AddReportsPresenter = new AddReportPresenter(this);
        }

      
        protected void btnSave_Click(object sender, EventArgs e)
        {


            try
            {
                if (validation())
                {
                    if (AddReportsItem != null)
                    {
                        AddReportsItem(sender, e);
                        //   ClearControls();
                    }
                    HttpContext.Current.Response.Redirect("~/Pages/Reports.aspx");

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
            return returnValue;
        }
        private void ClearControls()
        {
            //txtUrl.Text = string.Empty;
            txtDescription.InnerText = string.Empty;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Redirect("~/Pages/Reports.aspx");
        }

        
    }
}
