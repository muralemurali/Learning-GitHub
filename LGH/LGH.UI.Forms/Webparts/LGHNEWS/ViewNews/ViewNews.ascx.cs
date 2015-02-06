using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using LGH.UI.Forms.Presenter.NEWSLINKS;
using LGH.UI.Forms.Views.NEWSLINKS;
using LGH.Model.Utility;
using LGH.Model.Entities;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Web;

namespace LGH.UI.Forms.Webparts.LGHNEWS.ViewNews
{
    [ToolboxItemAttribute(false)]
    public partial class ViewNews : WebPart, INewsHomeView
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public ViewNews()
        {
        }

        #region Interface Implementation
        public event EventHandler LoadData;

        public IList<LGHNews> NewsInfoList
        {
            get;
            set;
        }
        #endregion

        #region variable declaration
        private ViewNewsPresenter _viewnewsPresenter;
        #endregion
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ChromeType = PartChromeType.None;
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _viewnewsPresenter= new ViewNewsPresenter(this);
            try
            {
                //If Page is postback then return
                if (Page.IsPostBack) return;

                LoadData(sender, e);

                if (NewsInfoList.Count > 0)
                {
                    gridCarousel.DataSource = NewsInfoList;
                    gridCarousel.DataBind();
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
                                spanCarousel.Attributes.CssStyle.Add("display", "none");
                                gridCarousel.Columns[gridCarousel.Columns.Count - 1].Visible = false;
                            }
                        }
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

        protected void gridCarousel_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var hyperLink = e.Row.FindControl("hpltitle") as HyperLink;
                var labellink = e.Row.FindControl("lbltitle") as Label;
                // var anchor = e.Row.FindControl("CarouselLink") as System.Web.UI.HtmlControls.HtmlAnchor;
                string[] urlvalue = labellink.Text.Contains(",") ? labellink.Text.Split(',') : new string[] { "", "" };


                if (hyperLink != null)
                {
                    hyperLink.NavigateUrl = urlvalue[0];
                    hyperLink.Text = urlvalue[1];
                }

              

            }


        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            string ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;
            HttpContext.Current.Response.Redirect("~/Pages/EditNews.aspx?ItemId=" + ids);
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
                            Classes.FormLogics.DeleteListItem(eWeb, int.Parse(ids), ListConfig.ListName.NewsList);
                        }

                    }
                });


                HttpContext.Current.Response.Redirect("~/Pages/News.aspx");

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
