using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using LGH.Model.Utility;
using LGH.Model.Entities;
using LGH.GENERAL.Presenter;
using LGH.GENERAL.Views;
using System.Collections.Generic;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebPartPages;


namespace LGH.GENERAL.Webparts.MeetingRoom.ExchangeCalendarMeeting
{
    [ToolboxItemAttribute(false)]
    public partial class ExchangeCalendarMeeting : System.Web.UI.WebControls.WebParts.WebPart,IExchangeCalendarView
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public ExchangeCalendarMeeting()
        {
        }

        //protected override void OnInit(EventArgs e)
        //{
        //    base.OnInit(e);
        //    InitializeControl();
        //}

      


        #region Interface Implementation
        public event EventHandler LoadData;

        public System.Collections.Generic.IList<LGHExchangeItems> ExchangeInfoList
        {
            get;
            set;
        }

        public System.Web.UI.WebControls.DropDownList LocationDropDownList
        {
            get { return drpMeetingRoom; }
        }

        public System.Collections.Generic.IList<LGHLocation> LocationInfoList
        {
            get;
            set;
        }
        #endregion

        #region Variable Declaration

        private ExchangeCalendarPresenter _calendarPresenter;
        private List<string> values = null;

        #endregion

        #region Constant Declaration

        private const string LocationTextField = "MeetingRoom";
        private const string LocationValueField = "MailBox";

        #endregion

        #region Event Handler

        /// <summary>
        /// Initialize the control
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ChromeType = PartChromeType.None;
            InitializeControl();
            //EnsureUpdatePanelFix();
        }

        /// <summary>
        /// Load the data from SP List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            _calendarPresenter = new ExchangeCalendarPresenter(this);

            try
            {
                //if (Page.IsPostBack) return;

                //divDate.InnerText = DateTime.Now.DayOfWeek.ToString() + ", " + DateTime.Now.ToString("MMMM dd, yyyy");
                LoadData(sender, e);
                BindLocation();

                //if (ddlLocation.Items.Count > 1)
                //{
                //    //_calendarPresenter.GetExchangeCalendarItems(ddlLocation.Items[1].Text);
                //    //BindDataIntoControls();
                //    LoadExchangeCalendar(ddlLocation.Items[1].Value);
                //    LoadCalendar();
                //    ddlLocation.SelectedIndex = 1;
                //}


                //if (!Page.IsPostBack)
                //{
                //    LoadCalendarAllItems();
                //    LoadCalendarInPageLoad();
                //}
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




        /// <summary>
        /// Update list view by the event type and load the calendar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
//        protected void gridCalendarView_RowCommand(object sender, GridViewCommandEventArgs e)
//        {
//            if (e.CommandName == "CreateView")
//            {
//                GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;

//                LinkButton typeName = (LinkButton)row.FindControl("lnkEdit");

//                if (typeName != null)
//                {
//                    SPSite site = new SPSite(SPContext.Current.Web.Url);
//                    SPWeb web = site.OpenWeb();
//                    web.AllowUnsafeUpdates = true;
//                    SPList eList = web.Lists["Location"];

//                    SPQuery qry = new SPQuery();
//                    qry.Query =
//                            @"   <Where>
//                                      <And>
//                                         <Eq>
//                                            <FieldRef Name='MeetingLocation' />
//                                            <Value Type='Text'>" + ddlLocation.SelectedItem.Text + @"</Value>
//                                         </Eq>
//                                         <Eq>
//                                            <FieldRef Name='MeetingRoom' />
//                                            <Value Type='Text'>" + typeName.Text + @"</Value>
//                                         </Eq>
//                                      </And>
//                                   </Where>";
//                    SPListItemCollection itemCollection = eList.GetItems(qry);
//                    if (itemCollection.Count > 0)
//                    {
//                        SPListItem listItem = itemCollection[0];
//                        if (listItem != null)
//                        {
//                            //CalendarSettings(listItem["Mail Box"].ToString());
//                            LoadExchangeCalendar(listItem["Mail Box"].ToString());
//                        }
//                    }
//                }
//            }
//        }

        /// <summary>
        /// Bind Location
        /// </summary>
        private void BindLocation()
        {
            if (LocationInfoList.Count > 0)
            {
                //List<SseLocation> distinctItems = LocationInfoList
                //                                 .GroupBy(s => s.Location)
                //                                 .Select(grp => grp.FirstOrDefault())
                //                                 .OrderBy(s => s.Location)
                //                                 .ToList();

                LocationDropDownList.DataSource = LocationInfoList;
                LocationDropDownList.DataTextField = LocationTextField;
                LocationDropDownList.DataValueField = LocationValueField;
                LocationDropDownList.DataBind();

                LocationDropDownList.Items.Insert(0, "Select");
            }
        }

        private void LoadExchangeCalendar(string mailBox)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWeb = oSite.OpenWeb())
                    {
                        string[] spliter = null;
                        SPList oList = oWeb.Lists["Exchange Calendar"];
                        SPList oLocationList = oWeb.Lists["Location"];
                        //SPListItemCollection oItemCollection = oList.Items;
                        //StringBuilder deletebuilder = BatchCommand(oList);
                        //oSite.RootWeb.ProcessBatchData(deletebuilder.ToString());
                        //SPListItem oItem = null;
                        //foreach (var item in values)
                        //{
                        //    spliter = item.Split(',');
                        //    oItem = oList.Items.Add();
                        //    oItem["Title"] = spliter[0];
                        //    oItem["Location"] = spliter[1];
                        //    oItem["Start Time"] = Convert.ToDateTime(spliter[2]);
                        //    oItem["End Time"] = Convert.ToDateTime(spliter[3]);
                        //    oItem.Update();
                        //}
                        string viewQuery = string.Empty;
                        viewQuery = @"   <Where>
      <Eq>
         <FieldRef Name='MailBox' />
         <Value Type='Text'>" + mailBox + @"</Value>
      </Eq>
   </Where>";
                        oWeb.AllowUnsafeUpdates = true;
                        SetCalendarView(oList.DefaultView, viewQuery);
                        oWeb.AllowUnsafeUpdates = false;
                        //var cal = LocationFiterView(oLocationList);
                    }
                }
            });
        }

        private void SetCalendarView(SPView view, string Query)
        {
            view.Query = Query;
            view.Update();
            view.ParentList.Update();
        }

        //private StringBuilder BatchCommand(SPList spList)
        //{
        //    StringBuilder deletebuilder = new StringBuilder();
        //    deletebuilder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Batch>");
        //    string command = "<Method><SetList Scope=\"Request\">" + spList.ID +
        //        "</SetList><SetVar Name=\"ID\">{0}</SetVar><SetVar Name=\"Cmd\">Delete</SetVar></Method>";

        //    foreach (SPListItem item in spList.Items)
        //    {
        //        deletebuilder.Append(string.Format(command, item.ID.ToString()));
        //    }
        //    deletebuilder.Append("</Batch>");
        //    return deletebuilder;
        //}

        //private void CalendarSettings(string mailboxUrl)
        //{
        //    //SPSecurity.RunWithElevatedPrivileges(delegate()
        //    //{
        //    System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);

        //    //string serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
        //    ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2013);
        //    service.Url = new Uri("https://mx.tillid.com/EWS/Exchange.asmx");
        //    //service.Credentials = new WebCredentials("meetingRoom01@tillid.com",string.Empty);
        //    //service.Url = new Uri(serviceUrl);
        //    FolderId te = new FolderId(WellKnownFolderName.Calendar, mailboxUrl);
        //    DateTime start = DateTime.Now;
        //    DateTime end = DateTime.Now.AddDays(30);
        //    CalendarView view = new CalendarView(start, end);
        //    FindItemsResults<Appointment> foundAppointments = service.FindAppointments(te, view);
        //    values = new List<string>();
        //    foreach (Appointment exchangeAppointment in foundAppointments)
        //    {
        //        values.Add(exchangeAppointment.Subject + "," + exchangeAppointment.Location + "," + exchangeAppointment.Start + "," + exchangeAppointment.End);
        //        //Console.WriteLine(exchangeAppointment.Location + "Date: " + exchangeAppointment.Start.ToString());
        //    }
        //    //});
        //}

        private List<LocationandMailBox> LocationFiterView(SPList List)
        {
            List<LocationandMailBox> settings = new List<LocationandMailBox>();

            SPListItemCollection oItemCollection = List.Items;
            foreach (SPListItem oItem in oItemCollection)
            {
                settings.Add(new LocationandMailBox
                {
                    Location = oItem["Location"].ToString(),
                    MailBox = oItem["Mail Box"].ToString()
                });

            }
            return settings;
        }

        //protected void gridCalendarView_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        e.Row.CssClass = "CalendarEventTypeClass" + e.Row.RowIndex;
        //    }
        //}

        //protected void btnGetDetails_Click(object sender, EventArgs e)
        //{
        //    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
        //    {
        //        using (SPWeb web = site.OpenWeb())
        //        {
        //            web.AllowUnsafeUpdates = true;

        //            var listName = ConfigurationManager.AppSettings["SseCalendar"];

        //            SPList list = web.Lists.TryGetList(listName);
        //            if (list != null)
        //            {

        //                SPFile spFile = web.GetFile("/Pages/SseCalendar.aspx");
        //                if (spFile != null)
        //                {
        //                    if (spFile.CheckOutType == SPFile.SPCheckOutType.None)
        //                    {
        //                        spFile.CheckOut();
        //                    }

        //                    SPLimitedWebPartManager myWebPartManager = spFile.GetLimitedWebPartManager(PersonalizationScope.Shared);
        //                    foreach (System.Web.UI.WebControls.WebParts.WebPart myWebPart in myWebPartManager.WebParts)
        //                    {
        //                        if (myWebPart.GetType().Name == "ListViewWebPart")
        //                        {
        //                            ListViewWebPart listWebPart = (ListViewWebPart)myWebPart;
        //                            SPView calendarView = list.Views["Calendar"];
        //                            //Guid myGuid = new Guid(listWebPart.ViewGuid);
        //                            SPView webPartView = calendarView;
        //                            webPartView.ViewFields.DeleteAll();

        //                            foreach (string strField in calendarView.ViewFields)
        //                            {
        //                                webPartView.ViewFields.Add(strField);
        //                            }
        //                            webPartView.Query = calendarView.Query;
        //                            webPartView.Update();

        //                            listWebPart.ViewGuid = webPartView.ID.ToString("B").ToUpper();
        //                            listWebPart.Visible = true;
        //                            listWebPart.Title = "SSE Calendar";
        //                            myWebPartManager.SaveChanges(listWebPart);
        //                            break;
        //                        }
        //                    }

        //                    //Check-in the layout
        //                    spFile.CheckIn("Checked In");
        //                    //Publish the layout
        //                    spFile.Publish("Publish");

        //                    web.AllowUnsafeUpdates = false;
        //                }
        //            }
        //        }
        //    }
        //    Page.Response.Redirect(Page.Request.Url.AbsoluteUri);
        //}

        #endregion

        #region Private Methods

        /// <summary>
        /// Bind Event Types into Gridview
        /// </summary>
        //private void BindDataIntoControls()
        //{
        //    if (ExchangeInfoList.Count <= 0) return;

        //    gridCalendarView.DataSource = ExchangeInfoList;
        //    gridCalendarView.DataBind();
        //}

        /// <summary>
        /// Load Calendar with updated views
        /// </summary>
        //private void LoadCalendarInPageLoad()
        //{
        //    SPFile spFile = null;
        //    try
        //    {
        //        using (SPSite site = new SPSite(SPContext.Current.Site.Url))
        //        {
        //            using (SPWeb web = site.OpenWeb())
        //            {
        //                web.AllowUnsafeUpdates = true;

        //                SPList list = web.Lists.TryGetList("Upcoming Events");
        //                if (list != null)
        //                {
        //                    spFile = web.GetFile(Page.Request.Url.AbsoluteUri);
        //                    if (spFile != null)
        //                    {
        //                        if (spFile.CheckOutType == SPFile.SPCheckOutType.None)
        //                        {
        //                            spFile.CheckOut();
        //                        }

        //                        SPLimitedWebPartManager myWebPartManager = spFile.GetLimitedWebPartManager(PersonalizationScope.Shared);
        //                        foreach (System.Web.UI.WebControls.WebParts.WebPart myWebPart in myWebPartManager.WebParts)
        //                        {
        //                            if (myWebPart.GetType().Name == "ListViewWebPart")
        //                            {
        //                                ListViewWebPart listWebPart = (ListViewWebPart)myWebPart;
        //                                SPView calendarView = list.Views["Calendar"];
        //                                Guid myGuid = new Guid(listWebPart.ViewGuid);
        //                                SPView webPartView = list.Views[myGuid];
        //                                webPartView.ViewFields.DeleteAll();

        //                                foreach (string strField in calendarView.ViewFields)
        //                                {
        //                                    webPartView.ViewFields.Add(strField);
        //                                }
        //                                webPartView.Query = calendarView.Query;
        //                                webPartView.Update();

        //                                listWebPart.ViewGuid = webPartView.ID.ToString("B").ToUpper();
        //                                listWebPart.Visible = true;
        //                                listWebPart.Title = "SSE Calendar";
        //                                myWebPartManager.SaveChanges(listWebPart);
        //                                break;
        //                            }
        //                        }

        //                        //Check-in the layout
        //                        spFile.CheckIn("Checked In");
        //                        //Publish the layout
        //                        spFile.Publish("Publish");

        //                        web.AllowUnsafeUpdates = false;
        //                    }
        //                    else
        //                    {
        //                        throw new Exception("Page does not exist");
        //                    }
        //                }
        //                else
        //                {
        //                    throw new Exception("List does not exist");
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (spFile.CheckOutType != SPFile.SPCheckOutType.None)
        //        {
        //            //Check-in the layout
        //            spFile.CheckIn("Checked In");
        //            //Publish the layout
        //            spFile.Publish("Publish");
        //        }
        //        Logger.LogError("SSE WebParts", ex.Message);
        //    }
        //}

        //private void LoadCalendar()
        //{
        //    SPFile spFile = null;
        //    try
        //    {
        //        SPSecurity.RunWithElevatedPrivileges(delegate()
        //        {
        //            using (SPSite site = new SPSite(SPContext.Current.Site.Url))
        //            {
        //                using (SPWeb web = site.OpenWeb())
        //                {
        //                    web.AllowUnsafeUpdates = true;

        //                    SPList list = web.Lists.TryGetList("Exchange Calendar");
        //                    if (list != null)
        //                    {
        //                        spFile = web.GetFile(Page.Request.Url.AbsoluteUri);
        //                        if (spFile != null)
        //                        {
        //                            if (spFile.CheckOutType == SPFile.SPCheckOutType.None)
        //                            {
        //                                spFile.CheckOut();
        //                            }

        //                            SPLimitedWebPartManager myWebPartManager = spFile.GetLimitedWebPartManager(PersonalizationScope.Shared);
        //                            foreach (System.Web.UI.WebControls.WebParts.WebPart myWebPart in myWebPartManager.WebParts)
        //                            {
        //                                if (myWebPart.GetType().Name == "ListViewWebPart")
        //                                {
        //                                    ListViewWebPart listWebPart = (ListViewWebPart)myWebPart;
        //                                    SPView calendarView = list.Views["Calendar"];
        //                                    Guid myGuid = new Guid(listWebPart.ViewGuid);
        //                                    SPView webPartView = list.Views[myGuid];
        //                                    webPartView.ViewFields.DeleteAll();

        //                                    foreach (string strField in calendarView.ViewFields)
        //                                    {
        //                                        webPartView.ViewFields.Add(strField);
        //                                    }
        //                                    webPartView.Query = calendarView.Query;
        //                                    webPartView.Update();

        //                                    listWebPart.ViewGuid = webPartView.ID.ToString("B").ToUpper();
        //                                    listWebPart.Visible = true;
        //                                    listWebPart.Title = "SSE Calendar";
        //                                    myWebPartManager.SaveChanges(listWebPart);
        //                                    break;
        //                                }
        //                            }

        //                            //Check-in the layout
        //                            spFile.CheckIn("Checked In");
        //                            //Publish the layout
        //                            spFile.Publish("Publish");

        //                            web.AllowUnsafeUpdates = false;
        //                        }
        //                        else
        //                        {
        //                            throw new Exception("Page does not exist");
        //                        }
        //                    }
        //                    else
        //                    {
        //                        throw new Exception("List does not exist");
        //                    }
        //                }
        //            }
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        if (spFile.CheckOutType != SPFile.SPCheckOutType.None)
        //        {
        //            //Check-in the layout
        //            spFile.CheckIn("Checked In");
        //            //Publish the layout
        //            spFile.Publish("Publish");
        //        }
        //        Logger.LogError("SSE WebParts", ex.Message);
        //    }
        //    //Page.Response.Redirect(Page.Request.Url.AbsoluteUri);
        //}

//        private void LoadCalendarAllItems()
//        {
//            using (SPSite site = new SPSite(SPContext.Current.Site.Url))
//            {
//                using (SPWeb web = site.OpenWeb())
//                {
//                    web.AllowUnsafeUpdates = true;

//                    SPList list = web.Lists.TryGetList("Upcoming Events");
//                    SPView CalendarView = list.Views["Calendar"];

//                    CalendarView.Query = @"<OrderBy>
//                                                    <FieldRef Name='SseEventType' />
//                                               </OrderBy>";

//                    CalendarView.Update();
//                    list.Update();

//                    web.AllowUnsafeUpdates = false;
//                }
//            }
//        }

        //private void EnsureUpdatePanelFix()
        //{
        //    if (this.Page.Form != null)
        //    {
        //        string formOnSubmitAtt = this.Page.Form.Attributes["onsubmit"];
        //        if (formOnSubmitAtt == "return _spFormOnSubmitWrapper);")
        //        {
        //            this.Page.Form.Attributes["onsubmit"] = "_spFormOnSubmitWrapper();";
        //        }
        //    }
        //    ScriptManager.RegisterStartupScript(this, typeof(SseExchangeCalendar),
        //        "UpdatePanelFixup",
        //        "_spOriginalFormAction = document.forms[0].action; _spSuppressFormOnSubmitWrapper=true;",
        //        true);
        //}

        #endregion

        //protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (drpMeetingRoom.SelectedItem.Text != "Select")
        //    {
        //        //_calendarPresenter.GetExchangeCalendarItems(ddlLocation.SelectedItem.Text);
        //        //BindDataIntoControls();

        //        LoadExchangeCalendar(drpMeetingRoom.SelectedValue);
        //        LoadCalendar();
        //    }
        //}
    }


    public class LocationandMailBox
    {
        public string Location { get; set; }
        public string MailBox { get; set; }
    }
}
