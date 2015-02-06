using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Publishing;
using Microsoft.SharePoint.Publishing.Fields;
using LGH.ListConfig;

namespace LGH.UI.HomePageTiles.Webparts.HomeTiles
{
    [ToolboxItemAttribute(false)]
    public partial class HomeTiles : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public HomeTiles()
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
            if(!Page.IsPostBack)
            {

         //   NavigationMenuDiv.InnerHtml = TopNavigationDiv();
            BannerCarouselDiv.InnerHtml = CarouselsDiv(); 
            HomeTileRow1.InnerHtml = HomeTileRowDiv1();
            HomeTileRow2.InnerHtml = HomeTileRowDiv2();
            }
            
        }

        private string CarouselsDiv()
        {
            string htmlBuilder = string.Empty;
            string ImageUrl = string.Empty;
            string linkURL = string.Empty;
            SPListItemCollection carouselItems = null;
            int count = 0;
            carouselItems = ListItemCollections(ListName.CarouselList);
            htmlBuilder += "<div id='myCarousel-1' class='carousel slide'  data-ride='carousel'>";

            if(carouselItems!=null)
            {
            if (carouselItems.Count > 0)
            {
                htmlBuilder += @"<ol class='carousel-indicators'>";
                foreach (SPListItem item in carouselItems)
                {
                   
                    if (count == 0)
                    {
                        htmlBuilder += @"<li data-target='#myCarousel-1' data-slide-to='0' class='active'> </li>";
                    }
                    else
                    {
                        htmlBuilder += @"<li data-target='#myCarousel-1' data-slide-to='"+count+"'> </li>";
                    }
                    count++;
                }
                htmlBuilder += "</ol>";
                htmlBuilder += @"<div class='carousel-inner' role='listbox'>";
                count = 0;
                foreach (SPListItem item in carouselItems)
                {

                    if (item[HomeTileConstants.ThumbnailImage] != null)
                    {
                        ImageFieldValue pageImage = item[HomeTileConstants.ThumbnailImage] as ImageFieldValue;
                        ImageUrl = pageImage.ImageUrl != null ? pageImage.ImageUrl : string.Empty;
                    }
                    SPFieldMultiLineText multilineField = item.Fields.GetField(HomeTileConstants.Description) as SPFieldMultiLineText;

                    string descriptionText = item[HomeTileConstants.Description] != null ? multilineField.GetFieldValueAsText(item[HomeTileConstants.Description].ToString()) : string.Empty;

                    if (descriptionText.Length > 160)
                    {
                        descriptionText = descriptionText.Substring(0, 155) + "...";
                    }
                    if (item[HomeTileConstants.Url] != null)
                    {
                        SPFieldUrlValue hyperlinkValue = new SPFieldUrlValue(item[HomeTileConstants.Url].ToString());
                        linkURL = hyperlinkValue.Url;
                    }
                    if (count == 0)
                    {
                        htmlBuilder += "<div class='item active'>";
                    }
                    else
                    {
                        htmlBuilder += "<div class='item'>";

                    }
                    count++;

                    htmlBuilder += @"<a target='_blank' href='"+linkURL+@"'>
                                                    <img src='"+ImageUrl+@"' alt='First slide' />
                                                </a>
                                                <div class='container'>
                                                    <div class='carousel-caption'>
                                                        <h1>
                                                            <a target='_blank' href='"+linkURL+"'>" + descriptionText + @"
                                                            </a>
                                                        </h1>
                                                    </div>
                                                </div>
                                            </div>";

                }
                htmlBuilder += "</div>";
                htmlBuilder += "</div>";
            }

            }


            return htmlBuilder;
           
        }

//        private string TopNavigationDiv()
//        {
//            string htmlBuilder = string.Empty;
//            htmlBuilder += @"<div class='sidebar menu'>
//                                    <ul id='menu'>
//                                        <li>
//                                            <a href='#'>HOME
//                                            </a>
//                                        </li>
//                                        <li class='active'>
//                                            <a href='#' class='hasdrop'>ACCESS MANAGEMENT
//                                            </a>
//                                            <ul id='drop' class='dropdown'>
//                                                <li>
//                                                    <a href='#'>APPLICATIONS
//                                                    </a>
//                                                </li>
//                                                <li>
//                                                    <a href='#'>ASSISTANCE
//                                                    </a>
//                                                </li>
//                                                <li>
//                                                    <a href='#'>APPLICATIONS
//                                                    </a>
//                                                </li>
//                                                <li>
//                                                    <a href='#'>ASSISTANCE
//                                                    </a>
//                                                </li>
//                                                <li>
//                                                    <a href='#'>APPLICATIONS
//                                                    </a>
//                                                </li>
//                                                <li>
//                                                    <a href='#'>ASSISTANCE
//                                                    </a>
//                                                </li>
//                                            </ul>
//                                        </li>
//                                        <li>
//                                            <a href='#'>APPLICATIONS
//                                            </a>
//                                        </li>
//                                        <li>
//                                            <a href='#'>ASSISTANCE
//                                            </a>
//                                        </li>
//                                        <li>
//                                            <a href='#' class='hasdrop'>CLINICALS
//                                            </a>
//                                            <ul id='Ul1' class='dropdown'>
//                                                <li>
//                                                    <a href='#'>APPLICATIONS
//                                                    </a>
//                                                </li>
//                                                <li>
//                                                    <a href='#'>ASSISTANCE
//                                                    </a>
//                                                </li>
//                                                <li>
//                                                    <a href='#'>APPLICATIONS
//                                                    </a>
//                                                </li>
//                                                <li>
//                                                    <a href='#'>ASSISTANCE
//                                                    </a>
//                                                </li>
//                                                <li>
//                                                    <a href='#'>APPLICATIONS
//                                                    </a>
//                                                </li>
//                                                <li>
//                                                    <a href='#'>ASSISTANCE
//                                                    </a>
//                                                </li>
//                                            </ul>
//                                        </li>
//                                        <li>
//                                            <a href='#'>EQUIPMENT &amp; SERVICES
//                                            </a>
//                                        </li>
//                                        <li>
//                                            <a href='#'>INFORMATION SECURITY
//                                            </a>
//                                        </li>
//                                        <li>
//                                            <a href='#' class='hasdrop'>IS OPERATIONS
//                                            </a>
//                                            <ul id='Ul2' class='dropdown'>
//                                                <li>
//                                                    <a href='#'>APPLICATIONS
//                                                    </a>
//                                                </li>
//                                                <li>
//                                                    <a href='#'>ASSISTANCE
//                                                    </a>
//                                                </li>
//                                                <li>
//                                                    <a href='#'>APPLICATIONS
//                                                    </a>
//                                                </li>
//                                                <li>
//                                                    <a href='#'>ASSISTANCE
//                                                    </a>
//                                                </li>
//                                                <li>
//                                                    <a href='#'>APPLICATIONS
//                                                    </a>
//                                                </li>
//                                                <li>
//                                                    <a href='#'>ASSISTANCE
//                                                    </a>
//                                                </li>
//                                            </ul>
//                                        </li>
//                                        <li>
//                                            <a href='#' class='hasdrop'>IT PRRIORITIES
//                                            </a>
//                                            <ul id='Ul3' class='dropdown'>
//                                                <li>
//                                                    <a href='#'>APPLICATIONS
//                                                    </a>
//                                                </li>
//                                                <li>
//                                                    <a href='#'>ASSISTANCE
//                                                    </a>
//                                                </li>
//                                                <li>
//                                                    <a href='#'>APPLICATIONS
//                                                   
//                                    </ul>
//                                </div>";

//            return htmlBuilder;
//        }

        private string HomeTileRowDiv1()
        {
            string htmlBuilder = string.Empty;
            try
            {
                htmlBuilder += ReportsDiv();
                htmlBuilder += NewsDiv();
                htmlBuilder += MeetingRoomsDiv();
            
            }
            catch (Exception ex)
            {
                
            }

            return htmlBuilder;
        }
        private string HomeTileRowDiv2()
        {
            string htmlBuilder = string.Empty;
            try
            {
                htmlBuilder += FAQDiv();
                htmlBuilder += HelpDeskDiv();
                htmlBuilder += StaffDirectoryDiv();

            }
            catch (Exception ex)
            {

            }

            return htmlBuilder;
        }

        private string StaffDirectoryDiv()
        {
            string htmlBuilder = string.Empty;
            string ImageUrl = string.Empty;
            string linkURL = string.Empty;
            SPListItem tileItem = null;
            tileItem = HomePageTileContents("6");
            if (tileItem != null)
            {
                if (tileItem[HomeTileConstants.ThumbnailImage] != null)
                {
                    ImageFieldValue pageImage = tileItem[HomeTileConstants.ThumbnailImage] as ImageFieldValue;
                    ImageUrl = pageImage.ImageUrl != null ? pageImage.ImageUrl : string.Empty;
                }
                 if (tileItem[HomeTileConstants.Url] != null)
                {
                    SPFieldUrlValue hyperlinkValue = new SPFieldUrlValue(tileItem[HomeTileConstants.Url].ToString());
                    linkURL = hyperlinkValue.Url;
                }
                htmlBuilder += @"  <div class='col-md-4 lpadding rpadding'>
                                        <div class='home_box'>
                                            <span>
                                                <a href='" + linkURL + @"'>
                                                    <img src='" + ImageUrl + @"' alt='reports' />
                                                </a>
                                            </span>
                                            <h3>" + tileItem[HomeTileConstants.Title].ToString() + @"
                                            </h3>";
                SPFieldMultiLineText multilineField = tileItem.Fields.GetField(HomeTileConstants.Description) as SPFieldMultiLineText;

                string descriptionText = tileItem[HomeTileConstants.Description] != null ? multilineField.GetFieldValueAsText(tileItem[HomeTileConstants.Description].ToString()) : string.Empty;

                if (descriptionText.Length > 115)
                {
                    descriptionText = descriptionText.Substring(0, 111) + "...";
                }


               

                htmlBuilder += @"<p>" + descriptionText + @"</p>
                                                <h2>
                                                <a href='" + linkURL + @"'>VIEW ALL
                                                </a>
                                            </h2>
                                        </div>
                                    </div>";
            }


            return htmlBuilder;
        }
        private string HelpDeskDiv()
        {
            string htmlBuilder = string.Empty;
            string ImageUrl = string.Empty;
            string linkURL = string.Empty;
            SPListItem tileItem = null;
            tileItem = HomePageTileContents("5");
            if (tileItem != null)
            {
                if (tileItem[HomeTileConstants.ThumbnailImage] != null)
                {
                    ImageFieldValue pageImage = tileItem[HomeTileConstants.ThumbnailImage] as ImageFieldValue;
                    ImageUrl = pageImage.ImageUrl != null ? pageImage.ImageUrl : string.Empty;
                }
                if (tileItem[HomeTileConstants.Url] != null)
                {
                    SPFieldUrlValue hyperlinkValue = new SPFieldUrlValue(tileItem[HomeTileConstants.Url].ToString());
                    linkURL = hyperlinkValue.Url;
                }
                htmlBuilder += @"  <div class='col-md-4 lpadding'>
                                        <div class='home_box'>
                                            <span>
                                                <a href='"+linkURL+@"'>
                                                    <img src='" + ImageUrl + @"' alt='reports' />
                                                </a>
                                            </span>
                                            <h3>" + tileItem[HomeTileConstants.Title].ToString() + @"
                                            </h3>";
                SPFieldMultiLineText multilineField = tileItem.Fields.GetField(HomeTileConstants.Description) as SPFieldMultiLineText;

                string descriptionText = tileItem[HomeTileConstants.Description] != null ? multilineField.GetFieldValueAsText(tileItem[HomeTileConstants.Description].ToString()) : string.Empty;

                if (descriptionText.Length > 115)
                {
                    descriptionText = descriptionText.Substring(0, 100) + "...";
                }


               

                htmlBuilder += @"<p>" + descriptionText + @"</p>
                                                <h2>
                                                <a href='" + linkURL + @"'>VIEW ALL
                                                </a>
                                            </h2>
                                        </div>
                                    </div>";
            }


            return htmlBuilder;
        }
        private string FAQDiv()
        {
            string htmlBuilder = string.Empty;
            string ImageUrl = string.Empty;
            string linkURL = string.Empty;
            SPListItem tileItem = null;
            tileItem = HomePageTileContents("4");
            if (tileItem != null)
            {
                if (tileItem[HomeTileConstants.ThumbnailImage] != null)
                {
                    ImageFieldValue pageImage = tileItem[HomeTileConstants.ThumbnailImage] as ImageFieldValue;
                    ImageUrl = pageImage.ImageUrl != null ? pageImage.ImageUrl : string.Empty;
                }
                if (tileItem[HomeTileConstants.Url] != null)
                {
                    SPFieldUrlValue hyperlinkValue = new SPFieldUrlValue(tileItem[HomeTileConstants.Url].ToString());
                    linkURL = hyperlinkValue.Url;
                }
                htmlBuilder += @"  <div class='col-md-4 lpadding'>
                                        <div class='home_box'>
                                            <span>
                                                <a href='" + linkURL + @"'>
                                                    <img src='" + ImageUrl + @"' alt='reports' />
                                                </a>
                                            </span>
                                            <h3>" + tileItem[HomeTileConstants.Title].ToString() + @"
                                            </h3>";
                SPFieldMultiLineText multilineField = tileItem.Fields.GetField(HomeTileConstants.Description) as SPFieldMultiLineText;

                string descriptionText = tileItem[HomeTileConstants.Description] != null ? multilineField.GetFieldValueAsText(tileItem[HomeTileConstants.Description].ToString()) : string.Empty;

                if (descriptionText.Length > 115)
                {
                    descriptionText = descriptionText.Substring(0, 100) + "...";
                }


               

                htmlBuilder += @"<p>" + descriptionText + @"</p>
                                                <h2>
                                                <a href='" + linkURL + @"'>VIEW ALL
                                                </a>
                                            </h2>
                                        </div>
                                    </div>";
            }


            return htmlBuilder;
        }
        private string MeetingRoomsDiv()
        {
            string htmlBuilder = string.Empty;
            string ImageUrl = string.Empty;
            string linkURL = string.Empty;
            SPListItem tileItem = null;
            tileItem = HomePageTileContents("3");
            if (tileItem != null)
            {
                if (tileItem[HomeTileConstants.ThumbnailImage] != null)
                {
                    ImageFieldValue pageImage = tileItem[HomeTileConstants.ThumbnailImage] as ImageFieldValue;
                    ImageUrl = pageImage.ImageUrl != null ? pageImage.ImageUrl : string.Empty;
                }
                if (tileItem[HomeTileConstants.Url] != null)
                {
                    SPFieldUrlValue hyperlinkValue = new SPFieldUrlValue(tileItem[HomeTileConstants.Url].ToString());
                    linkURL = hyperlinkValue.Url;
                }
                htmlBuilder += @"  <div class='col-md-4 lpadding rpadding'>
                                        <div class='home_box'>
                                            <span>
                                                <a href='" + linkURL + @"'>
                                                    <img src='" +ImageUrl+@"' alt='reports' />
                                                </a>
                                            </span>
                                            <h3>" + tileItem[HomeTileConstants.Title].ToString() + @"
                                            </h3>";
                SPFieldMultiLineText multilineField = tileItem.Fields.GetField(HomeTileConstants.Description) as SPFieldMultiLineText;

                string descriptionText = tileItem[HomeTileConstants.Description] != null ? multilineField.GetFieldValueAsText(tileItem[HomeTileConstants.Description].ToString()) : string.Empty;

                if (descriptionText.Length > 115)
                {
                    descriptionText = descriptionText.Substring(0, 111) + "...";
                }

               
               

                htmlBuilder += @"<p>" + descriptionText + @"</p>
                                                <h2>
                                                <a href='" + linkURL + @"'>VIEW ALL
                                                </a>
                                            </h2>
                                        </div>
                                    </div>";
            }


            return htmlBuilder;
            
        }
        private string NewsDiv()
        {
            string htmlBuilder = string.Empty;
            string ImageUrl = string.Empty;
            string linkURL = string.Empty;
            SPListItem tileItem = null;
            tileItem = HomePageTileContents("2");
            if (tileItem != null)
            {
                if (tileItem[HomeTileConstants.ThumbnailImage] != null)
                {
                    ImageFieldValue pageImage = tileItem[HomeTileConstants.ThumbnailImage] as ImageFieldValue;
                    ImageUrl = pageImage.ImageUrl != null ? pageImage.ImageUrl : string.Empty;
                }
                if (tileItem[HomeTileConstants.Url] != null)
                {
                    SPFieldUrlValue hyperlinkValue = new SPFieldUrlValue(tileItem[HomeTileConstants.Url].ToString());
                    linkURL = hyperlinkValue.Url;
                }
                htmlBuilder += @"  <div class='col-md-4 lpadding'>
                                        <div class='home_box'>
                                            <span>
                                                <a href='" + linkURL + @"'>
                                                    <img src='" +ImageUrl+@"' />
                                                </a>
                                            </span>
                                            <h3>" + tileItem[HomeTileConstants.Title].ToString() + @"
                                            </h3>";
                SPFieldMultiLineText multilineField = tileItem.Fields.GetField(HomeTileConstants.Description) as SPFieldMultiLineText;

                string descriptionText = tileItem[HomeTileConstants.Description] != null ? multilineField.GetFieldValueAsText(tileItem[HomeTileConstants.Description].ToString()) : string.Empty;

                if (descriptionText.Length > 115)
                {
                    descriptionText = descriptionText.Substring(0, 100) + "...";
                }

                
               

                htmlBuilder += @"<p>" + descriptionText + @"</p>
                                                <h2>
                                                <a href='" + linkURL + @"'>VIEW ALL
                                                </a>
                                            </h2>
                                        </div>
                                    </div>";
            }


            return htmlBuilder;
           
        }
        private string ReportsDiv()
        {
            string htmlBuilder = string.Empty;
            string ImageUrl = string.Empty;
            string linkURL = string.Empty;
            SPListItem tileItem = null;
            tileItem = HomePageTileContents("1");
            if (tileItem != null)
            {
                if (tileItem[HomeTileConstants.ThumbnailImage] != null)
                {
                    ImageFieldValue pageImage = tileItem[HomeTileConstants.ThumbnailImage] as ImageFieldValue;
                    ImageUrl = pageImage.ImageUrl != null ? pageImage.ImageUrl : string.Empty;
                }
                if (tileItem[HomeTileConstants.Url] != null)
                {
                    SPFieldUrlValue hyperlinkValue = new SPFieldUrlValue(tileItem[HomeTileConstants.Url].ToString());
                    linkURL = hyperlinkValue.Url;
                }
                htmlBuilder += @"  <div class='col-md-4 lpadding'>
                                        <div class='home_box'>
                                            <span>
                                                <a href='"+linkURL+@"'>
                                                    <img src='"+ImageUrl+@"' alt='reports' />
                                                </a>
                                            </span>
                                            <h3>"+tileItem[HomeTileConstants.Title].ToString() +@"
                                            </h3>";
                SPFieldMultiLineText multilineField = tileItem.Fields.GetField(HomeTileConstants.Description) as SPFieldMultiLineText;

                string descriptionText = tileItem[HomeTileConstants.Description] != null ? multilineField.GetFieldValueAsText(tileItem[HomeTileConstants.Description].ToString()): string.Empty;

                if (descriptionText.Length > 115)
                {
                    descriptionText = descriptionText.Substring(0, 100) + "...";
                }

               
                

                 htmlBuilder +=             @"<p>"+descriptionText+@"</p>
                                                <h2>
                                                <a href='"+linkURL+@"'>VIEW ALL
                                                </a>
                                            </h2>
                                        </div>
                                    </div>";
            }


            return htmlBuilder;
        }
        

        private SPListItem HomePageTileContents(string KeyValue)
        {
            SPListItem homeTileContents = null;
            
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                // get the site in impersonated context
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    // get the web in the impersonated context
                    using (SPWeb web = site.OpenWeb())
                    {
                          SPList spList = web.Lists.TryGetList(ListName.HomeTileListName);
                        if (spList != null)
                        {
                            SPQuery qry = new SPQuery();
                            qry.Query =@"   <Where>
                                      <Eq>
                                         <FieldRef Name='KeyValue' />
                                         <Value Type='Text'>"+KeyValue+@"</Value>
                                      </Eq>
                                   </Where>";
                            homeTileContents = spList.GetItems(qry)[0];
                        } 

                    }
                }
            });

                  return homeTileContents;


        }

        private SPListItemCollection ListItemCollections(string listName)
        {
            SPListItemCollection itemCollection = null;
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                // get the site in impersonated context
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    // get the web in the impersonated context
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList spList = web.Lists.TryGetList(listName);
                        if (spList != null)
                        {
                            itemCollection = spList.GetItems();
                        }

                    }
                }
            });


            return itemCollection;

        }
      


    }
}
