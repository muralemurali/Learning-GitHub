using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using System.Configuration;
using Microsoft.SharePoint;
namespace LGH.UI.HomePageTiles.Webparts.SocialMediaTopNavigation
{
    [ToolboxItemAttribute(false)]
    public partial class SocialMediaTopNavigation : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public SocialMediaTopNavigation()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                hdnSearchUrl.Value = SPContext.Current.Web.Url;
                if (!Page.IsPostBack)
                {
                    SocialMediaTopNavigationDiv.InnerHtml = LoadSocialMediaLinks();
                }
            }
            catch (Exception ex)
            {

            }

        }

        private string LoadSocialMediaLinks()
        {
            string htmlBuilder = string.Empty;
            SPListItemCollection socialmediaItemsCollec = null;
            string fbUrl = string.Empty;
            string twitterUrl = string.Empty;
            string googleplusUrl = string.Empty;
            string InstagramUrl = string.Empty;
            string youtubeUrl = string.Empty;
            string LinkedInUrl = string.Empty;

            try
            {
                
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite eSite = new SPSite(SPContext.Current.Site.Url))
                    {

                        using (SPWeb eWeb = eSite.OpenWeb())
                        {
                            // run elevated code here
                            SPList list = eWeb.Lists.TryGetList(ListConfig.ListName.SystemConfigurationList);
                            if (list != null)
                            {
                                socialmediaItemsCollec = list.GetItems();
                                if (socialmediaItemsCollec.Count > 0)
                                {
                                    foreach (SPListItem item in socialmediaItemsCollec)
                                    {
                                        if (item["Key"].ToString() == "FacebookUrl")
                                        {
                                            fbUrl = item["Title"].ToString();
                                        }
                                        if (item["Key"].ToString() == "TwitterUrl")
                                        {
                                            twitterUrl = item["Title"].ToString();
                                        }
                                        if (item["Key"].ToString() == "GoogleplusUrl")
                                        {
                                            googleplusUrl = item["Title"].ToString();
                                        }
                                        if (item["Key"].ToString() == "InstagramUrl")
                                        {
                                            InstagramUrl = item["Title"].ToString();
                                        }
                                        if (item["Key"].ToString() == "YoutubeUrl")
                                        {
                                            youtubeUrl = item["Title"].ToString();
                                        }
                                        if (item["Key"].ToString() == "LinkedInUrl")
                                        {
                                            LinkedInUrl = item["Title"].ToString();
                                        }
                                    }
                                }
                            }


                        }
                       
                    }
                });


                //fbUrl = System.Configuration.ConfigurationManager.AppSettings["FaceBookUrl"] == null ? "#" : System.Configuration.ConfigurationManager.AppSettings["FaceBookUrl"].ToString();
                // twitterUrl = System.Configuration.ConfigurationManager.AppSettings["TwitterUrl"] == null ? "#" : System.Configuration.ConfigurationManager.AppSettings["TwitterUrl"].ToString();
                // googleplusUrl = System.Configuration.ConfigurationManager.AppSettings["GooglePlusUrl"] == null ? "#" : System.Configuration.ConfigurationManager.AppSettings["GooglePlusUrl"].ToString();
                // InstagramUrl = System.Configuration.ConfigurationManager.AppSettings["InstagramUrl"] == null ? "#" : System.Configuration.ConfigurationManager.AppSettings["InstagramUrl"].ToString();
                // youtubeUrl = System.Configuration.ConfigurationManager.AppSettings["tumblrUrl"] == null ? "#" : System.Configuration.ConfigurationManager.AppSettings["tumblrUrl"].ToString();
                // LinkedInUrl = System.Configuration.ConfigurationManager.AppSettings["LinkedInUrl"] == null ? "#" : System.Configuration.ConfigurationManager.AppSettings["LinkedInUrl"].ToString();

                htmlBuilder += @" <button type='button' class='navbar-toggle collapsed' data-toggle='collapse' data-target='#navbar' aria-expanded='false' aria-controls='navbar'>
                                            <span class='sr-only'>Toggle navigation
                                            
                                            </span>
                                            <span class='icon-bar'>
                                            </span>
                                            <span class='icon-bar'>
                                            </span>
                                            <span class='icon-bar'>
                                            </span>
                                        </button>
                                        <a class='navbar-brand' href='/Pages/LGHome.aspx'>
                                            <img alt='LGH Logo' src='/Style Library/LGHCustomStyleSheet/images/LGH_logo.png' />
                                        </a>
                                    </div>
                                    <div class='col-sm-9 col-md-9 pull-right lpadding rpadding'>
                                        <ul class='social_media pull-right'>
                                            <li>
                                                <a target='_blank' href='"+fbUrl+ @"'>
                                                    <img alt='facebook' src='/Style Library/LGHCustomStyleSheet/images/fb.jpg' />
                                                </a>
                                            </li>
                                            <li>
                                                <a target='_blank' href='"+twitterUrl+ @"'>
                                                    <img alt='twitter' src='/Style Library/LGHCustomStyleSheet/images/twitter.jpg' />
                                                </a>
                                            </li>
                                            <li>
                                                <a target='_blank' href='"+googleplusUrl+ @"'>
                                                    <img alt='googleplus' src='/Style Library/LGHCustomStyleSheet/images/googleplus.jpg' />
                                                </a>
                                            </li>
                                            <li>
                                                <a target='_blank' href='"+InstagramUrl+ @"'>
                                                    <img alt='Instagram' src='/Style Library/LGHCustomStyleSheet/images/social.jpg' />
                                                </a>
                                            </li>
                                            <li>
                                                <a target='_blank' href='" + youtubeUrl + @"'>
                                                    <img alt='tumblr' src='/Style Library/LGHCustomStyleSheet/images/youtube.jpg' />
                                                </a>
                                            </li>
                                            <li>
                                                <a target='_blank' href='" + LinkedInUrl + @"'>
                                                    <img alt='LinkedIn' src='/Style Library/LGHCustomStyleSheet/images/linkedin.jpg' />
                                                </a>
                                            </li>

                                        </ul>
                                        <div class='clear'>
                                        </div>
                                        <div class='input-group'>
                                            <input type='text' class='form-control' placeholder='Type here and...' name='srch-term' id='srch-term' />
                                            <div class='input-group-btn'>
                                                <button class='btn btn-default' onclick='myFunction()' type='submit'>Search
                                                
                                                </button>
                                            </div>
                                        </div>";
            }
            catch (Exception ex)
            {


            }

            return htmlBuilder;
        }
    }
}
