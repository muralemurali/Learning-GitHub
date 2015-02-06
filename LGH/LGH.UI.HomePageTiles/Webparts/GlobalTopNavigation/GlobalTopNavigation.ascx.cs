using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Taxonomy;

namespace LGH.UI.HomePageTiles.Webparts.GlobalTopNavigation
{
    [ToolboxItemAttribute(false)]
    public partial class GlobalTopNavigation : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public GlobalTopNavigation()
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
                
                TopNavigationDiv.InnerHtml = TopNavigationContents();
            }
            catch (Exception ex)
            {

            }
        }

        private string HeaderContainerSocialMedia()
        {
            string htmlBuilder = string.Empty;

            htmlBuilder += @"<div class='row'>
                                <nav role='navigation'>
                                    <div class='col-sm-3 navbar-header lpadding'>
                                        <button type='button' class='navbar-toggle collapsed' data-toggle='collapse' data-target='#navbar' aria-expanded='false' aria-controls='navbar'>
                                            <span class='sr-only'>Toggle navigation
                                            
                                            </span>
                                            <span class='icon-bar'>
                                            </span>
                                            <span class='icon-bar'>
                                            </span>
                                            <span class='icon-bar'>
                                            </span>
                                        </button>
                                        <a class='navbar-brand' href='#'>
                                            <img alt='LGH Logo' src='/Style Library/LGHCustomStyleSheet/images/LGH_logo.png' />
                                        </a>
                                    </div>
                                    <div class='col-sm-9 col-md-9 pull-right lpadding rpadding'>
                                        <ul class='social_media pull-right'>
                                            <li>
                                                <a href='#contact'>
                                                    <img alt='facebook' src='/Style Library/LGHCustomStyleSheet/images/fb.jpg' />
                                                </a>
                                            </li>
                                            <li>
                                                <a href='#contact'>
                                                    <img alt='facebook' src='/Style Library/LGHCustomStyleSheet/images/twitter.jpg' />
                                                </a>
                                            </li>
                                            <li>
                                                <a href='#contact'>
                                                    <img alt='facebook' src='/Style Library/LGHCustomStyleSheet/images/googleplus.jpg' />
                                                </a>
                                            </li>
                                            <li>
                                                <a href='#contact'>
                                                    <img alt='facebook' src='/Style Library/LGHCustomStyleSheet/images/social.jpg' />
                                                </a>
                                            </li>
                                            <li>
                                                <a href='#contact'>
                                                    <img alt='facebook' src='/Style Library/LGHCustomStyleSheet/images/ticon.jpg' />
                                                </a>
                                            </li>
                                        </ul>
                                        <div class='clear'>
                                        </div>
                                        <div class='input-group'>
                                            <input type='text' class='form-control' placeholder='Type here and...' name='srch-term' id='srch-term' />
                                            <div class='input-group-btn'>
                                                <button class='btn btn-default'  type='submit'>Search
                                                
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                </nav>
                            </div>";

            return htmlBuilder;
        }



        private string TopNavigationContents()
        {
            string htmlBuilder = string.Empty;

            try
            {
                
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite eSite = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb eWeb = eSite.OpenWeb())
                        {
                            // run elevated code here
                            
                            htmlBuilder +=@"<ul id='menu'>";
                            TermSet TermsetName = Classes.Utility.GetTermSet(eSite,"LGH Navigation");
                            htmlBuilder += Classes.Utility.GetLIDetails(TermsetName);
                            htmlBuilder += "</ul>";
                        }
                        
                    }
                });
               
            }
            catch (Exception ex)
            {
              throw;
            }
            return htmlBuilder;
        }


    }
}
