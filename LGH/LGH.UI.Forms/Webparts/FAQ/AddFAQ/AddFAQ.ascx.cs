using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using LGH.Model.Utility;
using LGH.UI.Forms.Views;
using LGH.UI.Forms.Presenter;
using System.Web;


namespace LGH.UI.Forms.Webparts.FAQ.AddFAQ
{
    [ToolboxItemAttribute(false)]
    public partial class AddFAQ : WebPart,IAddFaqView
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public AddFAQ()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ChromeType = PartChromeType.None;
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _faqPresenter = new AddFaqPresenter(this);

            try
            {
                if (Page.IsPostBack) return;
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


        #region interface Implementation


        public event EventHandler SaveFaqItems;

        public string Question
        {
            get { return txtQuestion.InnerText; }
        }

        public string Answer
        {
            get { return txtAnswer.InnerText; }
        }
        #endregion

        #region Variable Declaration

        private AddFaqPresenter _faqPresenter;
        #endregion

        #region EventHandler


        /// <summary>
        /// Save Items into SharePoint List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (validation())
                {
                    if (SaveFaqItems != null)
                    {
                        SaveFaqItems(sender, e);
                        //     ClearControls();
                    }
                    HttpContext.Current.Response.Redirect("~/Pages/Faq.aspx");

                }

                // divMessage.InnerText = "Item Added Successfully";
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Redirect("~/Pages/Faq.aspx");
            
        }

    }
}
