using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;

namespace LGH.GENERAL.Webparts.LGHAdmin
{
    [ToolboxItemAttribute(false)]
    public partial class LGHAdmin : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public LGHAdmin()
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
                var lghAdminGroup = SPContext.Current.Web.Groups["LAFAYETTE GENERAL HEALTH ADMIN GROUP"];
                if (lghAdminGroup != null)
                {
                    if (SPContext.Current.Web.CurrentUser.LoginName == @"SHAREPOINT\system")
                    {
                        BindMenu();
                    }
                    else
                    {
                        if (!SPContext.Current.Web.IsCurrentUserMemberOfGroup(lghAdminGroup.ID))
                        {
                            var errorMessage = "You are not authorized to view this page. Please contact administrator.";
                            Page.Response.Redirect("~/Pages/Error.aspx?ErrorMessage=" + errorMessage);
                        }
                        else
                        {
                            BindMenu();
                        }
                    }
                }
            }
        }

        private void BindMenu()
        {
            divAdminMenu.InnerHtml = @"<div class='col-md-9 lpadding rpadding'>
				<div class='main_cont'>
				<h2>Admin Menu</h2>
					<div class='col-md-12'>
						<ul class='ad_menu'>
							<li><a  href='/Pages/Carousel.aspx'>Carousels</a></li>
							<li><a  href='/Pages/faq.aspx'>FAQ's</a></li>
							<li><a  href='/Shared Documents/Forms/AllItems.aspx '>Documents</a></li>
							<li><a  href='/_layouts/15/termstoremanager.aspx'>LGH Portal Navigation</a></li>
                            <li><a  href='/Pages/News.aspx'>News</a></li>
                            <li><a  href='/Pages/Reports.aspx'>Reports</a></li>  
							<li><a  href='/Pages/Forms/AllItems.aspx'>Pages</a></li>
							<li><a  href='/_layouts/15/viewlsts.aspx'>Site Contents</a></li>
							<li><a  href='/_layouts/15/settings.aspx'>Site Settings</a></li>
						</ul>
					</div>
	</div>
			</div>";
        }
    }
}
