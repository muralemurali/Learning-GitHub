using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using LGH.UI.Forms.Webparts.CAROUSEL.AddCarousel;
using LGH.UI.Forms.Webparts.CAROUSEL.ViewCarousel;
using LGH.Model.Utility;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint.WebPartPages;
using LGH.UI.Forms.Webparts.CAROUSEL.EditCarousel;
using LGH.UI.Forms.Webparts.FAQ.ViewFAQ;
using LGH.UI.Forms.Webparts.FAQ.AddFAQ;
using LGH.UI.Forms.Webparts.FAQ.EditFAQ;
using LGH.UI.Forms.Webparts.LGHREPORTS.AddLGHReports;
using LGH.UI.Forms.Webparts.LGHREPORTS.ViewReport;
using LGH.UI.Forms.Webparts.LGHREPORTS.EditReport;
using LGH.UI.Forms.Webparts.LGHNEWS.AddNews;
using LGH.UI.Forms.Webparts.LGHNEWS.ViewNews;
using LGH.UI.Forms.Webparts.LGHNEWS.EditNews;
using LGH.GENERAL.Webparts.LGHAdmin;
using LGH.GENERAL.Webparts.LGHErrorPage;
using LGH.GENERAL.Webparts.LGHMeetingRoom;
using LGH.StaffDirectory.StaffDirectory;
using LGH.UI.HomePageTiles.Webparts.HomeTiles;
using LGH.StaffDirectory.WebParts.StaffDirectory.SFDirectory;
using LGH.GENERAL.Webparts.LGHMeetingRoom;
using LGH.GENERAL.Webparts.LoadExchangeMeetingCalendar;




namespace LGH.Console.Features.LGH_PagesProvisioning
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("586974e7-8fcb-41a4-b35f-23a6dbfc64f0")]
    public class LGH_PagesProvisioningEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        //public override void FeatureActivated(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}


        #region Webpart Provisioning Logic
        #region Constant Declaration

        private const string PagesLibraryWebPartZoneId = "LGHPagesLibraryWebPart";
        private const string MeetingRoomWebpartZoneId1 = "LGHPagesLibraryWebPart";
        private const string MeetingRoomWebpartZoneId2 = "LGHMeetingRoomPageWebpart";

        #region All Custom WebParts Title

        //Forms Web Part
        private const string AddCarouselWebPartTitle = "Add Carousel";
        private const string ViewCarouselWebPartTitle = "View Carousel";
        private const string AddFaqtitle = "Add FAQ";
        private const string ViewFaqtitle = "View FAQ";
        private const string AddReportstitle = "Add Reports";
        private const string ViewReportsTitle = "View Reports";
        private const string AddNewsTitle = "Add News";
        private const string ViewNewsTitle = "View News";
        private const string EditCarouselWebpartTitle = "Edit Carousel";
        private const string EditFaqTitle = "Edit FAQ";
        private const string EditReportsTitle = "Edit Reports";
        private const string EditNewsTitle = "Edit News";
        private const string StaffDirectoryTitle = "Staff Directory";
        private const string AdminTitle = "LGH Admin Page";
        private const string ErrorTitle = "LGH Error Page";
        private const string HomePageWebpartTitle = "LGH Home Page Tiles";
        private const string MeetingRoomCalendarWebpartTitle = "Meeting Room Calendar";
        private const string MeetingRoomContentWebpartTitle = "Meeting Room Content";




        ////EuOfferings Web Part
        //private const string AddEuOfferingsWebPartTitle = "Add EuOfferings";
        //private const string ViewEuOfferingsWebPartTitle = "View EuOfferings";

        ////Upcoming Events Web Part
        //private const string AddUpcomingEventsWebPartTitle = "Add Upcoming Events";
        //private const string ViewUpcomingEventsWebPartTitle = "View Upcoming Events";

        ////SAC Events Web Part
        //private const string AddSacEventsWebPartTitle = "Add SAC Events";
        //private const string ViewSacEventsWebPartTitle = "View SAC Events";

        ////Sponsorship Web Part
        //private const string AddSponsorshipWebPartTitle = "Add Sponsorship";
        //private const string ViewSponsorshipWebPartTitle = "View Sponsorship";

        ////Discounts Web Part
        //private const string AddDiscountsWebPartTitle = "Add Discounts";
        //private const string ViewDiscountsWebPartTitle = "View Discounts";

        ////Faq Web Part
        //private const string AddFaqWebPartTitle = "Add Faq";
        //private const string ViewFaqWebPartTitle = "View Faq";

        #endregion

        #region Edit Web Part Title

        //private const string EditCarouselWebPartTitle = "Edit Carousel Banner";
        //private const string EditEuOfferingsWebPartTitle = "Edit EuOfferings";
        //private const string EditUpcomingEventsWebPartTitle = "Edit Upcoming Events";
        //private const string EditSacEventsWebPartTitle = "Edit SAC Events";
        //private const string EditSponsorshipPartTitle = "Edit Sponsorship";
        //private const string EditDiscountsWebPartTitle = "Edit Discounts";
        //private const string EditFaqWebPartTitle = "Edit Faq";

        #endregion

        #region Admin / Error Web Part Title

        ////Admin Page
        //private const string AdminPageWebPartTitle = "Admin Page";

        ////Error Page
        //private const string ErrorPageWebPartTitle = "Error Page";

        #endregion

        #region Add / View Web Part Pages Path

        //Carousel Web Part
        private const string AddCarouselPagePath = "/Pages/AddCarousel.aspx";
        private const string ViewCarouselPagePath = "/Pages/Carousel.aspx";
        private const string AddFaqPath = "/Pages/AddFaq.aspx";
        private const string ViewFaqPath = "/Pages/Faq.aspx";
        private const string AddReportsPath = "/Pages/AddReports.aspx";
        private const string ViewReportsPath = "/Pages/Reports.aspx";
        private const string AddNewsPath = "/Pages/AddNews.aspx";
        private const string ViewNewsPath = "/Pages/News.aspx";
        private const string EditCarouselWebpartPath = "/Pages/EditCarousel.aspx";
        private const string EditFaqPath = "/Pages/EditFaq.aspx";
        private const string EditReportsPath = "/Pages/EditReports.aspx";
        private const string EditNewsPath = "/Pages/EditNews.aspx";
        private const string StaffDirectoryPath = "/Pages/StaffDirectory.aspx";
        private const string AdminPath = "/Pages/LGHAdmin.aspx";
        private const string ErrorPath = "/Pages/Error.aspx";
        private const string HomePageWebpartPath = "/Pages/Home.aspx";
        private const string LGHomePath = "/Pages/LGHome.aspx";
        private const string MeetingRoomPath = "/Pages/MeetingRoom.aspx";


        ////EuOfferings Web Part
        //private const string AddEuOffringsPagePath = "/Pages/AddEuOfferings.aspx";
        //private const string ViewEuOfferingsPagePath = "/Pages/EuOfferings.aspx";

        ////Upcoming Events Web Part
        //private const string AddUpcomingEventsPagePath = "/Pages/AddUpcomingEvents.aspx";
        //private const string ViewUpcomingEventsPagePath = "/Pages/UpcomingEvents.aspx";

        ////SAC Events Web Part
        //private const string AddSacEventsPagePath = "/Pages/AddSacEvents.aspx";
        //private const string ViewSacEventsPagePath = "/Pages/SacEvents.aspx";

        ////Sponsorship Web Part
        //private const string AddSponsorshipPagePath = "/Pages/AddSponsorship.aspx";
        //private const string ViewSponsorshipPagePath = "/Pages/Sponsorship.aspx";

        ////Discounts Web Part
        //private const string AddDiscountsPagePath = "/Pages/AddDiscounts.aspx";
        //private const string ViewDiscountsPagePath = "/Pages/Discounts.aspx";

        ////Faq Web Part
        //private const string AddFaqPagePath = "/Pages/AddFaq.aspx";
        //private const string ViewFaqPagePath = "/Pages/Faq.aspx";

        #endregion

        #region Edit Web Part Pages Path

        //private const string EditCarouselPagePath = "/Pages/EditCarousel.aspx";
        //private const string EditEuOfferingsPagePath = "/Pages/EditEuOffers.aspx";
        //private const string EditUpcomingEventsPagePath = "/Pages/EditUpcomingEvents.aspx";
        //private const string EditSacEventsPagePath = "/Pages/EditSacEvents.aspx";
        //private const string EditSponsorshipPagePath = "/Pages/EditSponsorship.aspx";
        //private const string EditDiscountsPagePath = "/Pages/EditDiscounts.aspx";
        //private const string EditFaqPagePath = "/Pages/EditFaq.aspx";

        #endregion

        #region Sse Admin / Error Pages Path

        //private const string SseAdminPagePath = "/Pages/SseAdmin.aspx";
        //private const string SseErrorPagePath = "/Pages/Error.aspx";

        #endregion

        #region Upcoming Events Details

        ////Upcoming Events Details
        //private const string UpcomingDetailsPagePath = "/Pages/UpcomingDetails.aspx";
        //private const string UpcomingDetailsWebPartTitle = "Upcoming Details Web Part";

        #endregion

        #region Staff Directory

        ////Upcoming Events Details
        //private const string StaffDirectoryPagePath = "/Pages/StaffDirectory.aspx";
        //private const string StaffDirectoryWebPartTitle = "Staff Directory";

        #endregion

        #endregion

        #region Feature Activation

        /// <summary>
        /// Add Web Part to Page Layout
        /// </summary>
        /// <param name="properties"></param>
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            if (properties == null) return;

            //if properties is not null
            //Initialize SPWeb and SPSite as null
            SPWeb web = null;
            SPSite site = null;

            try
            {
                if (properties.Feature.Parent is SPWeb)
                {
                    //assign the feature properties parent as SPWeb
                    web = properties.Feature.Parent as SPWeb;
                }
                else
                {
                    //assign the feature properties parent as SPSite
                    site = properties.Feature.Parent as SPSite;
                    //get the web instance
                    if (site != null)
                    {
                        web = site.OpenWeb();
                    }
                }

                //LGH Home
                AddSaveViewWebPartToPageLayout(web, HomePageWebpartPath, HomePageWebpartTitle,
                    GetHomePageTilesWebPart(HomePageWebpartTitle));
                //LGH Sub Home
                AddSaveViewWebPartToPageLayout(web, LGHomePath, HomePageWebpartTitle,
                    GetHomePageTilesWebPart(HomePageWebpartTitle));
                //Add Carousel
                AddSaveViewWebPartToPageLayout(web, AddCarouselPagePath, AddCarouselWebPartTitle,
                    GetAddCarouselWebPart(AddCarouselWebPartTitle));

                //View Carousel
                AddSaveViewWebPartToPageLayout(web, ViewCarouselPagePath, ViewCarouselWebPartTitle,
                    GetViewCarouselWebPart(ViewCarouselWebPartTitle));

                //Edit Carousel
                AddSaveViewWebPartToPageLayout(web, EditCarouselWebpartPath, EditCarouselWebpartTitle,
                    GetEditCarouselWebPart(EditCarouselWebpartTitle));
                //Add FAQ
                AddSaveViewWebPartToPageLayout(web, AddFaqPath, AddFaqtitle,
                    GetAddFaqWebPart(AddFaqtitle));
                //Edit FAQ
                AddSaveViewWebPartToPageLayout(web, EditFaqPath, EditFaqTitle,
                    GetEditFaqWebPart(EditFaqTitle));
                //View FAQ
                AddSaveViewWebPartToPageLayout(web, ViewFaqPath, ViewFaqtitle,
                    GetViewFaqWebPart(ViewFaqtitle));
                //Add Reports
                AddSaveViewWebPartToPageLayout(web, AddReportsPath, AddReportstitle,
                    GetAddReportsWebPart(AddReportstitle));
                //Edit Reports
                AddSaveViewWebPartToPageLayout(web, EditReportsPath, EditReportsTitle,
                    GetEditReportsWebPart(EditReportsTitle));
                //View Reports
                AddSaveViewWebPartToPageLayout(web, ViewReportsPath, ViewReportsTitle,
                    GetViewReportsWebPart(ViewReportsTitle));
                //Add news
                AddSaveViewWebPartToPageLayout(web,AddNewsPath, AddNewsTitle,
                    GetAddNewsWebPart(AddNewsTitle));
                //View News
                AddSaveViewWebPartToPageLayout(web, ViewNewsPath, ViewNewsTitle,
                    GetViewNewsWebPart(ViewNewsTitle));
                //Edit News
                AddSaveViewWebPartToPageLayout(web, EditNewsPath, EditNewsTitle,
                    GetEditNewsWebPart(EditNewsTitle));
                //LGH Admin
                AddSaveViewWebPartToPageLayout(web, AdminPath, AdminTitle,
                    GetAdminWebPart(AdminTitle));
                //LGH ErrorPage
                AddSaveViewWebPartToPageLayout(web, ErrorPath, ErrorTitle,
                    GetErrorWebPart(ErrorTitle));

                //Staff Directory
                AddSaveViewWebPartToPageLayout(web, StaffDirectoryPath, StaffDirectoryTitle,
                    GetStaffDirectoryWebPart(StaffDirectoryTitle));
                //Staff Directory
                AddExchangeCalendarToMeetingRoomPage(properties);
                //AddSaveViewWebPartToPageLayout(web, MeetingRoomPath, MeetingRoomCalendarWebpartTitle,
                //    GetMeetingroomCalendarWebPart(MeetingRoomCalendarWebpartTitle));
                //Staff Directory
                AddSaveViewWebPartToPageLayout(web, MeetingRoomPath, MeetingRoomContentWebpartTitle,
                    GetMeetingroomContentsWebPart(StaffDirectoryTitle));


                

            }
            catch (Exception ex)
            {
                Logger.LogError("SSE WebParts", ex.Message);
            }
            finally
            {
                //Dispose site and web
                if (site != null)
                {
                    site.Dispose();
                }
                if (web != null)
                {
                    web.Dispose();
                }
            }
        }

        #endregion

        #region Get all WebParts Instance

        /// <summary>
        /// Get Add Carousel WebPart Instance
        /// </summary>
        /// <param name="webPartTitle"></param>
        /// <returns></returns>
        private static AddCarousel GetAddCarouselWebPart(string webPartTitle)
        {
            //Initialize Web Part Title and ChromeType
            var instance = new AddCarousel { Title = webPartTitle, ChromeType = PartChromeType.None };

            //Return the instance of the webpart
            return instance;
        }

        /// <summary>
        /// Get View Carousel WebPart Instance
        /// </summary>
        /// <param name="webPartTitle"></param>
        /// <returns></returns>
        private static ViewCarousel GetViewCarouselWebPart(string webPartTitle)
        {
            //Initialize Web Part Title and ChromeType
            var instance = new ViewCarousel { Title = webPartTitle, ChromeType = PartChromeType.None };

            //Return the instance of the webpart
            return instance;
        }

        /// <summary>
        /// Get Edit Carousel WebPart Instance
        /// </summary>
        /// <param name="webPartTitle"></param>
        /// <returns></returns>
        private static EditCarousel GetEditCarouselWebPart(string webPartTitle)
        {
            //Initialize Web Part Title and ChromeType
            var instance = new EditCarousel { Title = webPartTitle, ChromeType = PartChromeType.None };

            //Return the instance of the webpart
            return instance;
        }

        /// <summary>
        /// Get Add Faq WebPart Instance
        /// </summary>
        /// <param name="webPartTitle"></param>
        /// <returns></returns>
        private static AddFAQ GetAddFaqWebPart(string webPartTitle)
        {
            //Initialize Web Part Title and ChromeType
            var instance = new AddFAQ { Title = webPartTitle, ChromeType = PartChromeType.None };

            //Return the instance of the webpart
            return instance;
        }

        /// <summary>
        /// Get View Faq WebPart Instance
        /// </summary>
        /// <param name="webPartTitle"></param>
        /// <returns></returns>
        private static ViewFAQ GetViewFaqWebPart(string webPartTitle)
        {
            //Initialize Web Part Title and ChromeType
            var instance = new ViewFAQ { Title = webPartTitle, ChromeType = PartChromeType.None };

            //Return the instance of the webpart
            return instance;
        }

        /// <summary>
        /// Get View Faq WebPart Instance
        /// </summary>
        /// <param name="webPartTitle"></param>
        /// <returns></returns>
        private static EditFAQ GetEditFaqWebPart(string webPartTitle)
        {
            //Initialize Web Part Title and ChromeType
            var instance = new EditFAQ { Title = webPartTitle, ChromeType = PartChromeType.None };

            //Return the instance of the webpart
            return instance;
        }

        /// <summary>
        /// Get Add news WebPart Instance
        /// </summary>
        /// <param name="webPartTitle"></param>
        /// <returns></returns>
        private static AddNews GetAddNewsWebPart(string webPartTitle)
        {
            //Initialize Web Part Title and ChromeType
            var instance = new AddNews { Title = webPartTitle, ChromeType = PartChromeType.None };

            //Return the instance of the webpart
            return instance;
        }
        /// <summary>
        /// Get View News WebPart Instance
        /// </summary>
        /// <param name="webPartTitle"></param>
        /// <returns></returns>
        private static ViewNews GetViewNewsWebPart(string webPartTitle)
        {
            //Initialize Web Part Title and ChromeType
            var instance = new ViewNews { Title = webPartTitle, ChromeType = PartChromeType.None };

            //Return the instance of the webpart
            return instance;
        }
        /// <summary>
        /// Get EditNews  WebPart Instance
        /// </summary>
        /// <param name="webPartTitle"></param>
        /// <returns></returns>
        private static EditNews GetEditNewsWebPart(string webPartTitle)
        {
            //Initialize Web Part Title and ChromeType
            var instance = new EditNews { Title = webPartTitle, ChromeType = PartChromeType.None };

            //Return the instance of the webpart
            return instance;
        }
        /// <summary>
        /// Get Add Reports WebPart Instance
        /// </summary>
        /// <param name="webPartTitle"></param>
        /// <returns></returns>
        private static AddLGHReports GetAddReportsWebPart(string webPartTitle)
        {
            //Initialize Web Part Title and ChromeType
            var instance = new AddLGHReports { Title = webPartTitle, ChromeType = PartChromeType.None };

            //Return the instance of the webpart
            return instance;
        }
        /// <summary>
        /// Get View Reports WebPart Instance
        /// </summary>
        /// <param name="webPartTitle"></param>
        /// <returns></returns>
        private static ViewReport GetViewReportsWebPart(string webPartTitle)
        {
            //Initialize Web Part Title and ChromeType
            var instance = new ViewReport { Title = webPartTitle, ChromeType = PartChromeType.None };

            //Return the instance of the webpart
            return instance;
        }
        /// <summary>
        /// Get edit reports  WebPart Instance
        /// </summary>
        /// <param name="webPartTitle"></param>
        /// <returns></returns>
        private static EditReport GetEditReportsWebPart(string webPartTitle)
        {
              //Initialize Web Part Title and ChromeType
            var instance = new EditReport { Title = webPartTitle, ChromeType = PartChromeType.None };

            //Return the instance of the webpart
            return instance;
        }

        /// <summary>
        /// Get Admin  WebPart Instance
        /// </summary>
        /// <param name="webPartTitle"></param>
        /// <returns></returns>
        private static LGHAdmin GetAdminWebPart(string webPartTitle)
        {
            //Initialize Web Part Title and ChromeType
            var instance = new LGHAdmin { Title = webPartTitle, ChromeType = PartChromeType.None };

            //Return the instance of the webpart
            return instance;
        }
         /// <summary>
        /// Get Error  WebPart Instance
        /// </summary>
        /// <param name="webPartTitle"></param>
        /// <returns></returns>
        private static LGHErrorPage GetErrorWebPart(string webPartTitle)
        {
            //Initialize Web Part Title and ChromeType
            var instance = new LGHErrorPage { Title = webPartTitle, ChromeType = PartChromeType.None };

            //Return the instance of the webpart
            return instance;
        }

             /// <summary>
        /// Get HomePage  WebPart Instance
        /// </summary>
        /// <param name="webPartTitle"></param>
        /// <returns></returns>
        private static HomeTiles GetHomePageTilesWebPart(string webPartTitle)
        {
            //Initialize Web Part Title and ChromeType
            var instance = new HomeTiles { Title = webPartTitle, ChromeType = PartChromeType.None };

            //Return the instance of the webpart
            return instance;
        }

        /// <summary>
        /// Get Staff Directory  WebPart Instance
        /// </summary>
        /// <param name="webPartTitle"></param>
        /// <returns></returns>
        private static SFDirectory GetStaffDirectoryWebPart(string webPartTitle)
        {
            //Initialize Web Part Title and ChromeType
            var instance = new SFDirectory { Title = webPartTitle, ChromeType = PartChromeType.None };

            //Return the instance of the webpart
            return instance;
        }

        /// <summary>
        /// Get MeetingRoom WebPart Instance
        /// </summary>
        /// <param name="webPartTitle"></param>
        /// <returns></returns>
        private static LoadExchangeMeetingCalendar GetMeetingroomCalendarWebPart(string webPartTitle)
        {
            //Initialize Web Part Title and ChromeType
            var instance = new LoadExchangeMeetingCalendar { Title = webPartTitle, ChromeType = PartChromeType.None };

            //Return the instance of the webpart
            return instance;
        }

        /// <summary>
        /// Get MeetingRoom WebPart Instance
        /// </summary>
        /// <param name="webPartTitle"></param>
        /// <returns></returns>
        private static LGHMeetingRoom GetMeetingroomContentsWebPart(string webPartTitle)
        {
            //Initialize Web Part Title and ChromeType
            var instance = new LGHMeetingRoom { Title = webPartTitle, ChromeType = PartChromeType.None };

            //Return the instance of the webpart
            return instance;
        }
        #endregion

        

        #region Add Web Parts to Page Layout

        /// <summary>
        /// Add Save / View Web Parts to Pages Library for the General Page Layouts
        /// </summary>
        /// <param name="web"></param>
        /// <param name="pagePath"></param>
        /// <param name="webPartTitle"></param>
        /// <param name="values"></param>

        private static void AddExchangeCalendarToMeetingRoomPage(SPFeatureReceiverProperties Properties)
        {


            SPSite site = Properties.Feature.Parent as SPSite;
            using (SPWeb web = site.RootWeb)
            {
                using (SPLimitedWebPartManager wpManager = web.GetLimitedWebPartManager("/Pages/MeetingRoom.aspx", System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared))
                {
                    //Get Page Layout File
                    var layoutFile = web.GetFile("/Pages/MeetingRoom.aspx");

                    //CHeckout Layout File to edit
                    if (layoutFile.CheckOutType == SPFile.SPCheckOutType.None)
                    {
                        layoutFile.CheckOut();
                    }
                    web.AllowUnsafeUpdates = true;
                    web.Update();
                    SPList l = web.Lists["Exchange Calendar"];
                    ListViewWebPart wp = new ListViewWebPart();
                    wp.ListName = l.ID.ToString("B").ToUpper();
                    wp.ViewGuid = l.DefaultView.ID.ToString("B").ToUpper();

                    //wp.ZoneID = "Top";
                    wp.Title = "Meeting Room Calendar";
                    wpManager.AddWebPart(wp, "LGHPagesLibraryWebPart", 0);

                    //Check-in the layout
                    layoutFile.CheckIn("Checked In");
                    //Publish the layout
                    layoutFile.Publish("Publish");


                }
            }
        }


        private static void AddSaveViewWebPartToPageLayout(SPWeb web, string pagePath, string webPartTitle, params object[] values)
        {
            //Get Page Layout File
            var layoutFile = web.GetFile(pagePath);

            //CHeckout Layout File to edit
            if (layoutFile.CheckOutType == SPFile.SPCheckOutType.None)
            {
                layoutFile.CheckOut();
            }

            //Webpart Manager to get all the webparts in the page
            var webPartManager = layoutFile.GetLimitedWebPartManager(PersonalizationScope.Shared);

            bool[] webPartExists = { false };

            if (webPartManager != null)
            {
                IsWebPartExists(webPartManager, ref webPartExists[0], webPartTitle);
            }

            //Loop through each web part and set exists property to true
            foreach (var webpart in values)
            {
                //Add  Carousel Home WebPart
                if (webpart is AddCarousel && !webPartExists[0])
                {
                    var addCarouselWebPart = webpart as AddCarousel;
                    if (webPartManager == null) continue;
                    //Add the web part
                    webPartManager.AddWebPart(addCarouselWebPart, PagesLibraryWebPartZoneId, 0);
                    //Save the changes
                    webPartManager.SaveChanges(addCarouselWebPart);
                    //Set Web Part Exist as true
                    webPartExists[0] = true;
                }
                //View  Carousel Home WebPart
                else if (webpart is ViewCarousel && !webPartExists[0])
                {
                    var viewCarouselWebPart = webpart as ViewCarousel;
                    if (webPartManager == null) continue;
                    //Add the web part
                    webPartManager.AddWebPart(viewCarouselWebPart, PagesLibraryWebPartZoneId, 0);
                    //Save the changes
                    webPartManager.SaveChanges(viewCarouselWebPart);
                    //Set Web Part Exist as true
                    webPartExists[0] = true;
                }
                else if (webpart is EditCarousel && !webPartExists[0])
                {
                    var EditCarouselWebPart = webpart as EditCarousel;
                    if (webPartManager == null) continue;
                    //Add the web part
                    webPartManager.AddWebPart(EditCarouselWebPart, PagesLibraryWebPartZoneId, 0);
                    //Save the changes
                    webPartManager.SaveChanges(EditCarouselWebPart);
                    //Set Web Part Exist as true
                    webPartExists[0] = true;
                }
                else if (webpart is EditCarousel && !webPartExists[0])
                {
                    var EditCarouselWebPart = webpart as EditCarousel;
                    if (webPartManager == null) continue;
                    //Add the web part
                    webPartManager.AddWebPart(EditCarouselWebPart, PagesLibraryWebPartZoneId, 0);
                    //Save the changes
                    webPartManager.SaveChanges(EditCarouselWebPart);
                    //Set Web Part Exist as true
                    webPartExists[0] = true;
                }
                else if (webpart is AddFAQ && !webPartExists[0])
                {
                    var AddFaqwebpart = webpart as AddFAQ;
                    if (webPartManager == null) continue;
                    //Add the web part
                    webPartManager.AddWebPart(AddFaqwebpart, PagesLibraryWebPartZoneId, 0);
                    //Save the changes
                    webPartManager.SaveChanges(AddFaqwebpart);
                    //Set Web Part Exist as true
                    webPartExists[0] = true;
                }
                else if (webpart is ViewFAQ && !webPartExists[0])
                {
                    var ViewFaqwebpart = webpart as ViewFAQ;
                    if (webPartManager == null) continue;
                    //Add the web part
                    webPartManager.AddWebPart(ViewFaqwebpart, PagesLibraryWebPartZoneId, 0);
                    //Save the changes
                    webPartManager.SaveChanges(ViewFaqwebpart);
                    //Set Web Part Exist as true
                    webPartExists[0] = true;
                }
                else if (webpart is EditFAQ && !webPartExists[0])
                {
                    var EditFaqWebpart = webpart as EditFAQ;
                    if (webPartManager == null) continue;
                    //Add the web part
                    webPartManager.AddWebPart(EditFaqWebpart, PagesLibraryWebPartZoneId, 0);
                    //Save the changes
                    webPartManager.SaveChanges(EditFaqWebpart);
                    //Set Web Part Exist as true
                    webPartExists[0] = true;
                }
                else if (webpart is AddNews && !webPartExists[0])
                {
                    var AddNewsWebpart = webpart as AddNews;
                    if (webPartManager == null) continue;
                    //Add the web part
                    webPartManager.AddWebPart(AddNewsWebpart, PagesLibraryWebPartZoneId, 0);
                    //Save the changes
                    webPartManager.SaveChanges(AddNewsWebpart);
                    //Set Web Part Exist as true
                    webPartExists[0] = true;
                }
                else if (webpart is ViewNews && !webPartExists[0])
                {
                    var ViewNewsWebpart = webpart as ViewNews;
                    if (webPartManager == null) continue;
                    //Add the web part
                    webPartManager.AddWebPart(ViewNewsWebpart, PagesLibraryWebPartZoneId, 0);
                    //Save the changes
                    webPartManager.SaveChanges(ViewNewsWebpart);
                    //Set Web Part Exist as true
                    webPartExists[0] = true;
                }
                else if (webpart is EditNews && !webPartExists[0])
                {
                    var EditNewsWebpart = webpart as EditNews;
                    if (webPartManager == null) continue;
                    //Add the web part
                    webPartManager.AddWebPart(EditNewsWebpart, PagesLibraryWebPartZoneId, 0);
                    //Save the changes
                    webPartManager.SaveChanges(EditNewsWebpart);
                    //Set Web Part Exist as true
                    webPartExists[0] = true;
                }
                else if (webpart is AddLGHReports && !webPartExists[0])
                {
                    var AddReportswebpart = webpart as AddLGHReports;
                    if (webPartManager == null) continue;
                    //Add the web part
                    webPartManager.AddWebPart(AddReportswebpart, PagesLibraryWebPartZoneId, 0);
                    //Save the changes
                    webPartManager.SaveChanges(AddReportswebpart);
                    //Set Web Part Exist as true
                    webPartExists[0] = true;
                }
                else if (webpart is ViewReport && !webPartExists[0])
                {
                    var ViewReportswebpart = webpart as ViewReport;
                    if (webPartManager == null) continue;
                    //Add the web part
                    webPartManager.AddWebPart(ViewReportswebpart, PagesLibraryWebPartZoneId, 0);
                    //Save the changes
                    webPartManager.SaveChanges(ViewReportswebpart);
                    //Set Web Part Exist as true
                    webPartExists[0] = true;
                }
                else if (webpart is EditReport && !webPartExists[0])
                {
                    var EditReportswebpart = webpart as EditReport;
                    if (webPartManager == null) continue;
                    //Add the web part
                    webPartManager.AddWebPart(EditReportswebpart, PagesLibraryWebPartZoneId, 0);
                    //Save the changes
                    webPartManager.SaveChanges(EditReportswebpart);
                    //Set Web Part Exist as true
                    webPartExists[0] = true;
                }
                else if (webpart is LGHAdmin && !webPartExists[0])
                {
                    var LGHAdminWebpart = webpart as LGHAdmin;
                    if (webPartManager == null) continue;
                    //Add the web part
                    webPartManager.AddWebPart(LGHAdminWebpart, PagesLibraryWebPartZoneId, 0);
                    //Save the changes
                    webPartManager.SaveChanges(LGHAdminWebpart);
                    //Set Web Part Exist as true
                    webPartExists[0] = true;
                }
                else if (webpart is LGHErrorPage && !webPartExists[0])
                {
                    var LGHErrorWebpart = webpart as LGHErrorPage;
                    if (webPartManager == null) continue;
                    //Add the web part
                    webPartManager.AddWebPart(LGHErrorWebpart, PagesLibraryWebPartZoneId, 0);
                    //Save the changes
                    webPartManager.SaveChanges(LGHErrorWebpart);
                    //Set Web Part Exist as true
                    webPartExists[0] = true;
                }
                else if (webpart is SFDirectory && !webPartExists[0])
                {
                    var StaffDirectoryWebpart = webpart as SFDirectory;
                    if (webPartManager == null) continue;
                    //Add the web part
                    webPartManager.AddWebPart(StaffDirectoryWebpart, PagesLibraryWebPartZoneId, 0);
                    //Save the changes
                    webPartManager.SaveChanges(StaffDirectoryWebpart);
                    //Set Web Part Exist as true
                    webPartExists[0] = true;
                }
                else if (webpart is HomeTiles && !webPartExists[0])
                {
                    var HomePageTilesWebpart = webpart as HomeTiles;
                    if (webPartManager == null) continue;
                    //Add the web part
                    webPartManager.AddWebPart(HomePageTilesWebpart, PagesLibraryWebPartZoneId, 0);
                    //Save the changes
                    webPartManager.SaveChanges(HomePageTilesWebpart);
                    //Set Web Part Exist as true
                    webPartExists[0] = true;
                }
                else if (webpart is LGHMeetingRoom && !webPartExists[0])
                {
                    var MeetingRoomContentWebpart = webpart as LGHMeetingRoom;
                    if (webPartManager == null) continue;
                    //Add the web part
                    webPartManager.AddWebPart(MeetingRoomContentWebpart, PagesLibraryWebPartZoneId, 0);
                    //Save the changes
                    webPartManager.SaveChanges(MeetingRoomContentWebpart);
                    //Set Web Part Exist as true
                    webPartExists[0] = true;
                }
                else if (webpart is LoadExchangeMeetingCalendar && !webPartExists[0])
                {
                    var MeetingRoomCalendarWebpart = webpart as LoadExchangeMeetingCalendar;
                    if (webPartManager == null) continue;
                    //Add the web part
                    webPartManager.AddWebPart(MeetingRoomCalendarWebpart, PagesLibraryWebPartZoneId, 0);
                    //Save the changes
                    webPartManager.SaveChanges(MeetingRoomCalendarWebpart);
                    //Set Web Part Exist as true
                    webPartExists[0] = true;
                }
               
            }

            //Check-in the layout
            layoutFile.CheckIn("Checked In");
            //Publish the layout
            layoutFile.Publish("Publish");
        }

     

        #endregion

        #region Check Web Part Exists

        /// <summary>
        /// Check the web part is exist
        /// </summary>
        /// <param name="webPartManager"></param>
        /// <param name="webPartExist"></param>
        /// <param name="webPartTitle"></param>
        private static void IsWebPartExists(SPLimitedWebPartManager webPartManager, ref bool webPartExist, string webPartTitle)
        {
            //Loop through each webparts and set exists property
            foreach (System.Web.UI.WebControls.WebParts.WebPart webpart in webPartManager.WebParts)
            {
                //Carousel WebPart
                if (webpart != null && webpart.Title != null && webpart.Title.Equals(webPartTitle, StringComparison.CurrentCultureIgnoreCase))
                {
                    //Web part found
                    webPartExist = true;
                }
            }
        }

        #endregion

        #region Feature Deactivation

        /// <summary>
        /// Delete Web Part from the layout page
        /// </summary>
        /// <param name="properties"></param>
        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            //if (properties == null) return;

            ////if properties is not null
            ////Initialize SPWeb and SPSite as null
            //SPWeb web = null;
            //SPSite site = null;
            //if (properties.Feature.Parent is SPWeb)
            //{
            //    //assign the feature properties parent as SPWeb
            //    web = properties.Feature.Parent as SPWeb;
            //}
            //else
            //{
            //    //assign the feature properties parent as SPSite
            //    site = properties.Feature.Parent as SPSite;
            //    //get the web instance
            //    if (site != null)
            //    {
            //        web = site.OpenWeb();
            //    }
            //}

            //if (web != null)
            //{
            //    string[] pagesPath = {AddCarouselPagePath, ViewCarouselPagePath};

            //    foreach (var path in pagesPath)
            //    {
            //        var spFileHome = web.GetFile(path);
            //        var webPartManagerHome = spFileHome.GetLimitedWebPartManager(PersonalizationScope.Shared);

            //        //Get all Web Part and delete all the web part in home page layout
            //        var webPartListHome = from WebPart webPart in webPartManagerHome.WebParts select webPart;
            //        foreach (var webPart in webPartListHome.ToList())
            //        {
            //            webPartManagerHome.DeleteWebPart(webPart);
            //        }
            //    }
            //}
            //if (web == null) return;
            //web.Update();

            ////Dispose site and web object
            //if (site != null)
            //{
            //    site.Dispose();
            //}
            //web.Dispose();
        }

        #endregion




        

        #endregion
    }
}
