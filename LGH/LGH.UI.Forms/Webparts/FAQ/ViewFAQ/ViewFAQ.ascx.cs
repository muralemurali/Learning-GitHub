using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using LGH.Model.Utility;
using LGH.Model.Entities;
using LGH.UI.Forms.Views;
using LGH.UI.Forms.Presenter;
using Microsoft.SharePoint;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web;
using LGH.Model.ListDataSource;
using LGH.ListConfig;
using LGH.UI.Forms.Classes;


namespace LGH.UI.Forms.Webparts.FAQ.ViewFAQ
{
    [ToolboxItemAttribute(false)]
    public partial class ViewFAQ : WebPart,IFaqView
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public ViewFAQ()
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



            _viewFAQPresenter = new ViewFAQPresenter(this);
            try
            {
                //If Page is postback then return
                if (Page.IsPostBack) return;
                
                //If Page is not postback then load the data from SharePoint List and Bind to the control
                LoadData(sender, e);

                BindDataIntoControls();
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

        #region Private Methods

        /// <summary>
        /// Bind the values into control from SponsorshipInfoList object
        /// </summary>
        private void BindDataIntoControls()
        {

            if (FAQInfoList.Count <= 0) return;

            //spanFaq.Attributes.CssStyle.Add("display", "none");
            // dt = ToDataTable<SseFaqItems>(FAQInfoList);

            gridFAQ.DataSource = FAQInfoList;
            gridFAQ.DataBind();

            var lghAdminGroup = SPContext.Current.Web.Groups["LAFAYETTE GENERAL HEALTH ADMIN GROUP"];
            if (lghAdminGroup != null)
            {
                if (SPContext.Current.Web.CurrentUser.LoginName == @"SHAREPOINT\system")
                {
                    //do nothing
                }
                else
                {
                    if (!SPContext.Current.Web.IsCurrentUserMemberOfGroup(lghAdminGroup.ID))
                    {
                        spanFaq.Attributes.CssStyle.Add("display", "none");
                        gridFAQ.Columns[gridFAQ.Columns.Count - 1].Visible = false;
                    }
                }
            }

           

           
        }

        #endregion  

        #region Variable Declaration

        private ViewFAQPresenter _viewFAQPresenter;


        #endregion

        #region Interface Implementation

        /// <summary>
        /// Event Handler to Load Data
        /// </summary>
        public event EventHandler LoadData;

        public  event EventHandler DeleteFAQItem;
    

        /// <summary>
        /// Get and Set Values into SponsorshipInfoList object
        /// </summary>
        public IList<LGHFAQItems>FAQInfoList
        {
            get;
            set;
        }

        #endregion

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
           string ids = string.Empty;
           ids = (sender as LinkButton).CommandArgument;
           HttpContext.Current.Response.Redirect("~/Pages/EditFaq.aspx?ItemId=" + ids);

        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
           

            try
            {
                string ids = string.Empty;
                ids = (sender as LinkButton).CommandArgument;
              
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite eSite = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb eWeb = eSite.OpenWeb())
                        {
                            // run elevated code here
                            Classes.FormLogics.DeleteListItem(eWeb,int.Parse(ids) , ListConfig.ListName.FAQ);
                        }
                        
                    }
                });
                   
                    
                    HttpContext.Current.Response.Redirect("~/Pages/Faq.aspx");
                
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

    }
}
