using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;


namespace LGH.UI.HomePageTiles.Webparts.GlobalBottomNavigation
{
    [ToolboxItemAttribute(false)]
    public partial class GlobalBottomNavigation : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public GlobalBottomNavigation()
        {

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Page.IsPostBack)
            {
                BottomNavigationDiv.InnerHtml = FooterContent();
            }
        }

        private string FooterContent()
        {
            string htmlBuilder = string.Empty;
            SPListItemCollection FooterContentCollection = null;
            SPListItemCollection FooterHeadingCollection = null;
            
            int count = 1;

            try
            {

                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite eSite = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb eWeb = eSite.OpenWeb())
                        {
                            // run elevated code here.
                            FooterHeadingCollection = Classes.Utility.FooterHeadings(eWeb, ListConfig.ListName.FooterHeadingsList);
                            if (FooterHeadingCollection.Count > 0)
                            {
                                foreach (SPListItem headerItem in FooterHeadingCollection)
                                {
                                    string linkURL = string.Empty;
                                    FooterContentCollection = Classes.Utility.LookUpQuery(eWeb, headerItem[HomeTileConstants.Title].ToString(), ListConfig.ListName.FooterContentsList);
                                 
                                    if (FooterContentCollection.Count > 0)
                                    {
                                        if (count == 4)
                                        {
                                            htmlBuilder += @"<div class='col-md-3 lpadding rpadding'>";
                                        }
                                        else
                                        {
                                            htmlBuilder += @"<div class='col-md-3 lpadding'>";
                                        }
                                        if (headerItem[HomeTileConstants.Title].ToString() == "ABOUT LAFAYETTE GENERAL" && FooterContentCollection.Count == 1)
                                        {
                                            if (FooterContentCollection[0][HomeTileConstants.Url] != null)
                                            {
                                                SPFieldUrlValue hyperlinkValue = new SPFieldUrlValue(FooterContentCollection[0][HomeTileConstants.Url].ToString());
                                                linkURL = hyperlinkValue.Url;
                                            }
                                            string menuContent = FooterContentCollection[0][HomeTileConstants.FooterMenuContents].ToString();
                                            htmlBuilder += @"<div class='footer_cont'>
                                                                                <h3>" + headerItem[HomeTileConstants.Title].ToString() + @"
                                    
                                                                                </h3>
                                                                                <p>" + menuContent + @"
                                                                                </p>
                                                                                <h5>
                                                                                <a href='"+linkURL+@"'>More</a>
                                                                                </h5>
                                                                            </div>
                                                                        </div>";
                                        }
                                        else
                                        {
                                            htmlBuilder += @"<div class='footer_cont'>
                                                            <h3>" + headerItem[HomeTileConstants.Title].ToString() + @"</h3>
                                                            <ul>";

                                            foreach (SPListItem contentItem in FooterContentCollection)
                                            {
                                                linkURL = string.Empty;
                                                if (contentItem[HomeTileConstants.Url] != null)
                                                {
                                                    SPFieldUrlValue hyperlinkValue = new SPFieldUrlValue(contentItem[HomeTileConstants.Url].ToString());
                                                    linkURL = hyperlinkValue.Url;
                                                }

                                                htmlBuilder += @"<li>
                                                        <a href='"+linkURL+@"'>
                                                        " + contentItem[HomeTileConstants.FooterMenuContents].ToString() + @"                                                                                                                       </a>
                                                         </li>";

                                            }
                                            htmlBuilder += "</ul></div></div>";
                                        }

                                    }
                                    count++;
                                }
                            }
                        }



                    }

                });
            }

            catch (Exception ex)
            {

            }

            return htmlBuilder;


        }


    }
}
