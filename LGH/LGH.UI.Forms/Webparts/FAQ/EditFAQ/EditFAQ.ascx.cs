using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using LGH.Model.Utility;
using LGH.Model.Entities;
using LGH.UI.Forms.Views;
using LGH.UI.Forms.Presenter;
using System.Collections.Generic;
using System.Web;



namespace LGH.UI.Forms.Webparts.FAQ.EditFAQ
{
    [ToolboxItemAttribute(false)]
    public partial class EditFAQ : WebPart,IEditFAQView
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public EditFAQ()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ChromeType = PartChromeType.None;
            InitializeControl();
        }

        #region Variable Declaration

        private EditFAQPresenter _EditFAQPresenter;

        #endregion

        #region Interface Implementation

        public event EventHandler LoadData;

        public event EventHandler UpdateFAQItem;

        public IList<LGHFAQItems> FAQListInfo
        {
            get;
            set;
        }

        public string QueryStringItemId
        {
            get { return hdnItemId.Value; }
        }

        public string FaqAnswer
        {
            get { return txtAnswer.InnerText; }
        }

        public string FaqQuest
        {
            get { return txtQuestion.InnerText; }
        }
        #endregion

        #region Event Handler

      
        protected void Page_Load(object sender, EventArgs e)
        {
            _EditFAQPresenter = new EditFAQPresenter(this);

            try
            {
                if (Page.IsPostBack) return;

                if (Page.Request.QueryString["ItemId"] != null)
                {
                    hdnItemId.Value = Page.Request.QueryString["ItemId"].ToString();
                   //hdnItemId.Value = "3";

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
                Logger.LogError("SSE WebParts", ex.Message);
            }
        }

        private void BindDataIntoControls()
        {
            //Check if FAQListInfo object has values or not, if not return
            if (FAQListInfo.Count <= 0) return;

            //If FAQListInfo object has values, then bind the values into the control
            txtQuestion.InnerText = FAQListInfo[0].Question;
            txtAnswer.InnerText = FAQListInfo[0].Answer;



        }

        /// <summary>
        /// Update Item into SharePoint List
        /// </summary>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (validation())
                {

                    if (UpdateFAQItem != null)
                    {
                        UpdateFAQItem(sender, e);
                    }

                    HttpContext.Current.Response.Redirect("~/Pages/Faq.aspx");
                    
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
            lblAnswer.Text = "";
            lblQuestion.Text = "";
            if (txtQuestion.InnerText == string.Empty)
            {
                returnValue = false;
                lblQuestion.Text = "Please Enter the Question..!!";
            }
            if (txtAnswer.InnerText == string.Empty)
            {
                returnValue = false;
                lblAnswer.Text = "Please Enter the Answer..!!";
            }
            return returnValue;
        }


        #endregion
    }
}
