using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace LGH.GENERAL.EventReceiver.ExchangeCalendarViewCreationER
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class ExchangeCalendarViewCreationER : SPItemEventReceiver
    {
        /// <summary>
        /// An item was added.
        /// </summary>
        public override void ItemAdded(SPItemEventProperties properties)
        {
            // base.ItemAdded(properties);
            CreateViewinExchangeCalander(properties);
        }

        private void CreateViewinExchangeCalander(SPItemEventProperties properties)
        {
            using (SPWeb web = properties.OpenWeb())
            {

                SPListItem CurrentItem = properties.ListItem;
                SPList oList = web.Lists["Exchange Calendar"];
                if (CurrentItem["MeetingRoom"] != null)
                {
                    if (!IsViewAvailble(oList, CurrentItem["MeetingRoom"].ToString()))
                    {
                        SPView newView = oList.DefaultView;
                        newView = newView.Clone(CurrentItem["MeetingRoom"].ToString(), 0, true, false);
                        oList.Update();
                    }
                }

            }
        }

        private bool IsViewAvailble(SPList exchangelist, string viewName)
        {
            bool IsValid = false;

            SPViewCollection oViewCollection = exchangelist.Views;
            foreach (SPView oView in oViewCollection)
            {
                if (oView.Title == viewName)
                {
                    IsValid = true;
                }
            }

            return IsValid;

        }

    }
}