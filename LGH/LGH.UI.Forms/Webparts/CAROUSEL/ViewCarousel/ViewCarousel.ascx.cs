﻿using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using LGH.UI.Forms.Presenter;
using LGH.UI.Forms.Views;
using LGH.Model.Utility;
using LGH.Model.Entities;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Web;

namespace LGH.UI.Forms.Webparts.CAROUSEL.ViewCarousel
{
    [ToolboxItemAttribute(false)]
    public partial class ViewCarousel : WebPart,ICarouselHomeView
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public ViewCarousel()
        {
        }

        #region Variable Declaration
        private ViewRepPresenter _viewCarouselPresenter;

        #endregion

        #region Interface Implementation

        public event EventHandler LoadData;

        public IList<CarouselHomePage> CarouselInfoList
        {
            get;
            set;
        }

        #endregion
        #region Event Handler

        /// <summary>
        /// Initialize the controls
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            //Set Web Part Chrome Type to None
            ChromeType = PartChromeType.None;
            InitializeControl();
        }

        /// <summary>
        /// Load the data from SP List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            _viewCarouselPresenter = new ViewRepPresenter(this);
            try
            {
                //If Page is postback then return
                if (Page.IsPostBack) return;

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

        #endregion

        #region Private Methods

        private void BindDataIntoControls()
        {
            //Check if SponsorshipInfoList object has values or not, if not return
            if (CarouselInfoList.Count <= 0) return;

            var imageUrl = string.Empty;
            for (var i = 0; i < CarouselInfoList.Count; i++)
            {
                imageUrl = Regex.Match(CarouselInfoList[i].CarouselImage, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                CarouselInfoList[i].CarouselImage = imageUrl;
            }

            gridCarousel.DataSource = CarouselInfoList;
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

            //var sseAdminGroup = SPContext.Current.Web.Groups["SSE Admin"];
            //if (sseAdminGroup != null)
            //{
            //    if (SPContext.Current.Web.CurrentUser.LoginName == @"SHAREPOINT\system")
            //    {
            //        //do nothing
            //    }
            //    else
            //    {
            //        if (!SPContext.Current.Web.IsCurrentUserMemberOfGroup(sseAdminGroup.ID))
            //        {
            //            spanCarousel.Attributes.CssStyle.Add("display", "none");
            //            gridCarousel.Columns[gridCarousel.Columns.Count - 1].Visible = false;
            //        }
            //    }
            //}
        }

        #endregion

        protected void gridCarousel_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var hyperLink = e.Row.FindControl("hpltitle") as HyperLink;
                var labellink = e.Row.FindControl("lbltitle") as Label;
                var anchor = e.Row.FindControl("CarouselLink") as System.Web.UI.HtmlControls.HtmlAnchor;
                string[] urlvalue = labellink.Text.Contains(",") ? labellink.Text.Split(',') : new string[] { "", "" };


                if (hyperLink != null)
                    hyperLink.NavigateUrl = urlvalue[0];

                if (anchor != null)
                {
                    anchor.HRef = urlvalue[0];
                }
                    
            }


        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            string ids = string.Empty;
            ids = (sender as LinkButton).CommandArgument;
            HttpContext.Current.Response.Redirect("~/Pages/EditCarousel.aspx?ItemId=" + ids);
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
                            Classes.FormLogics.DeleteListItem(eWeb, int.Parse(ids), ListConfig.ListName.CarouselList);
                        }

                    }
                });


                HttpContext.Current.Response.Redirect("~/Pages/Carousel.aspx");

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
