using LGH.Model.Utility;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebPartPages;
using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;

namespace LGH.GENERAL.Webparts.LGHMeetingRoom
{
    [ToolboxItemAttribute(false)]
    public partial class LGHMeetingRoom : System.Web.UI.WebControls.WebParts.WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public LGHMeetingRoom()
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
            if (!Page.IsPostBack)
            {
                LoadMeetingRoomDetails();
                LoadCalendar(string.Empty, drpMeetingRoom.SelectedValue);
            }
        }

        private void LoadMeetingRoomDetails()
        {
            SPListItemCollection itemCollection = null;
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite eSite = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb eWeb = eSite.OpenWeb())
                    {
                        itemCollection = Classes.MeetingRoomInfo.ListItemCollection(eWeb, "LGH-Meeting-Location");
                        if (itemCollection != null)
                        {
                            if (itemCollection.Count > 0)
                            {
                                foreach (SPListItem item in itemCollection)
                                {
                                    drpMeetingRoom.Items.Add(item["MeetingRoom"].ToString());
                                }
                            }
                        }
                    }
                }
            });
        }

        protected void drpMeetingRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            string viewQuery = string.Empty;
            SPSecurity.RunWithElevatedPrivileges(delegate()
           {
               using (SPSite eSite = new SPSite(SPContext.Current.Site.Url))
               {
                   using (SPWeb eWeb = eSite.OpenWeb())
                   {
                       SPList list = eWeb.Lists.TryGetList("Exchange Calendar");
                       if (list != null)
                       {
                           viewQuery = @"   <Where>
      <Eq>
         <FieldRef Name='MailBox' />
         <Value Type='Text'>" + drpMeetingRoom.SelectedValue + @"</Value>
      </Eq>
   </Where>";
                       }
                   }

               }

               LoadCalendar(viewQuery, drpMeetingRoom.SelectedValue);
           });
        }


        private void LoadCalendar(string queryView, string selectedValue)
        {
            SPFile spFile = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            web.AllowUnsafeUpdates = true;

                            SPList list = web.Lists.TryGetList("Exchange Calendar");
                            if (list != null)
                            {
                                spFile = web.GetFile(Page.Request.Url.AbsoluteUri);
                                if (spFile != null)
                                {
                                    if (spFile.CheckOutType == SPFile.SPCheckOutType.None)
                                    {
                                        spFile.CheckOut();
                                    }

                                    SPLimitedWebPartManager myWebPartManager = spFile.GetLimitedWebPartManager(PersonalizationScope.Shared);
                                    foreach (System.Web.UI.WebControls.WebParts.WebPart myWebPart in myWebPartManager.WebParts)
                                    {
                                        if (myWebPart.GetType().Name == "ListViewWebPart")
                                        {
                                            ListViewWebPart listWebPart = (ListViewWebPart)myWebPart;
                                            SPView calendarView = null;
                                            if (selectedValue != "select")
                                            {
                                                calendarView = list.Views[selectedValue];
                                            }
                                            else
                                            {
                                                calendarView = list.DefaultView;
                                                queryView = string.Empty;
                                            }
                                            Guid myGuid = new Guid(listWebPart.ViewGuid);
                                            SPView webPartView = list.Views[myGuid];
                                            webPartView.ViewFields.DeleteAll();

                                            foreach (string strField in calendarView.ViewFields)
                                            {
                                                webPartView.ViewFields.Add(strField);
                                            }
                                            webPartView.Query = queryView;
                                            webPartView.Update();

                                            listWebPart.ViewGuid = webPartView.ID.ToString("B").ToUpper();
                                            listWebPart.Visible = true;
                                            // listWebPart.Title = "SSE Calendar";
                                            myWebPartManager.SaveChanges(listWebPart);
                                            break;
                                        }
                                    }

                                    //Check-in the layout
                                    spFile.CheckIn("Checked In");
                                    //Publish the layout
                                    spFile.Publish("Publish");

                                    web.AllowUnsafeUpdates = false;
                                }
                                else
                                {
                                    throw new Exception("Page does not exist");
                                }
                            }
                            else
                            {
                                throw new Exception("List does not exist");
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                if (spFile.CheckOutType != SPFile.SPCheckOutType.None)
                {
                    //Check-in the layout
                    spFile.CheckIn("Checked In");
                    //Publish the layout
                    spFile.Publish("Publish");
                }
                Logger.LogError("SSE WebParts", ex.Message);
            }
            //Page.Response.Redirect(Page.Request.Url.AbsoluteUri);
        }
    }
}
