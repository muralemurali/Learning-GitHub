using LGH.Model.IRepositories;
using LGH.Model.ListDataSource;
using System.Configuration;
using System.Data;
using LGH.Model.Entities;
using System.Collections.Generic;
using System;
using LGH.Data.Constants;

namespace LGHData.Repositories
{
    public class ExchangeCalendarRepository : IExchangeCalendarRepository
    {
        #region Variable Declaration

        private List<LGHExchangeItems> _exchangeCalendarInfo;
        private List<LGHLocation> _locationInfo;

        #endregion



#region ConstantDeclaration


#endregion
        #region Public Methods

        /// <summary>
        /// Get Location Details
        /// </summary>
        /// <returns></returns>
        public IList<LGHLocation> GetLocationInfoList()
        {
            _locationInfo = new List<LGHLocation>();

            //Get UpcomingEventsList Url from web.config file
            //Ex. <add key="UpcomingEventsList" value="http://servername/Lists/Upcoming%20Events/AllItems.aspx" />
            //var listUrl = ConfigurationManager.AppSettings["LocationList"];
            var listUrl=LGH.ListConfig.ListName.LocationList;
            //Initialize the object of Listdatasource class and verify the site and list is valid for the given Url
            var list = new ListDataSource(listUrl);

            //CAML Query for getting item from List
            const string query = "<Where>" +
                                    "<IsNotNull>" +
                                        "<FieldRef Name='MeetingRoom' />" +
                                    "</IsNotNull>" +
                                 "</Where>";

            const string viewFields = "<FieldRef Name='" + Constants.MailBox + "' /><FieldRef Name='" + Constants.MeetingRoom + "' />";

            //Set the attribute to 'Recursive' to get data from inside folder also
            //const string viewAttributes = "Scope='Recursive'";
            var viewAttributes = string.Empty;

            //Get the item from list based on the CAML query
            var locationTable = list.QueryList(query, viewFields, viewAttributes, -1);

            //Dispose the objects (SPWeb and SPSite)
            list.Dispose();

            //If upcomingEventsTable table row count is <= 0, then return empty object
            if (locationTable.Rows.Count <= 0) return _locationInfo;

            //If upcomingEventsTable table row count is > 0, then construct entity from table
            ConstructLocation(locationTable);

            return _locationInfo;
        }

        /// <summary>
        /// Get Exchange Calendar Info from SharePoint List
        /// </summary>
        /// <returns></returns>
        /// <param name="locationName"></param>
        public IList<LGHExchangeItems> GetExchangeEventsInfoList(string locationName)
        {
            _exchangeCalendarInfo = new List<LGHExchangeItems>();

            //Get UpcomingEventsList Url from web.config file
            //Ex. <add key="UpcomingEventsList" value="http://servername/Lists/Upcoming%20Events/AllItems.aspx" />
            //var listUrl = ConfigurationManager.AppSettings["LocationList"];

            //Initialize the object of Listdatasource class and verify the site and list is valid for the given Url
            var listUrl=LGH.ListConfig.ListName.LocationList;
            var list = new ListDataSource(listUrl);

            //CAML Query for getting item from List
            var query = "<Where>" +
                                    //"<And>" +
                                    //    "<IsNotNull>" +
                                    //        "<FieldRef Name='MeetingRoom' />" +
                                    //    "</IsNotNull>" +
                                        "<Eq>" +
                                            "<FieldRef Name='MeetingRoom' />" +
                                            "<Value Type='Text'>" +
                                               locationName +
                                            "</Value>" +
                                        "</Eq>" +
                                    //"</And>" +
                                 "</Where>";

            const string viewFields =
                "<FieldRef Name='" + Constants.ItemId + "' />" +
                "<FieldRef Name='" + Constants.MailBox + "' />" +
                "<FieldRef Name='" + Constants.MeetingRoom + "' />";
               


            //Set the attribute to 'Recursive' to get data from inside folder also
            //const string viewAttributes = "Scope='Recursive'";
            var viewAttributes = string.Empty;

            //Get the item from list based on the CAML query
            var exchangeTable = list.QueryList(query, viewFields, viewAttributes, -1);

            //Dispose the objects (SPWeb and SPSite)
            list.Dispose();

            //If exchangeTable table row count is <= 0, then return empty object
            if (exchangeTable.Rows.Count <= 0) return _exchangeCalendarInfo;

            //If exchangeTable table row count is > 0, then construct entity from table
            ConstructExchangeCalendar(exchangeTable);

            return _exchangeCalendarInfo;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Add resultset table into entity
        /// </summary>
        /// <param name="exchangeTable"></param>
        private void ConstructExchangeCalendar(DataTable exchangeTable)
        {
            for (var i = 0; i < exchangeTable.Rows.Count; i++)
            {
                //Set the current Row
                var currentRow = exchangeTable.Rows[i];

                //Initializ the object and assign the value into entity field
                var entity = new LGHExchangeItems
                {
                    ItemId =
                        currentRow[Constants.ItemId] is DBNull
                            ? 0
                            : Convert.ToInt64(currentRow[Constants.ItemId]),
                    ExchangeMailbox =
                        currentRow[Constants.MailBox] is DBNull
                            ? string.Empty
                            : (string)currentRow[Constants.MailBox],
                    MeetingRoom =
                        currentRow[Constants.MeetingRoom] is DBNull
                            ? string.Empty
                            : (string)currentRow[Constants.MeetingRoom]
                   
                };

                //Add the entity value into the list
                _exchangeCalendarInfo.Add(entity);
            }
        }

        /// <summary>
        /// Add resultset table into entity
        /// </summary>
        /// <param name="locationTable"></param>
        private void ConstructLocation(DataTable locationTable)
        {
            for (var i = 0; i < locationTable.Rows.Count; i++)
            {
                //Set the current Row
                var currentRow = locationTable.Rows[i];

                //Initializ the object and assign the value into entity field
                var entity = new LGHLocation
                {
                    MailBox =
                        currentRow[Constants.MailBox] is DBNull
                            ? string.Empty
                            : (string)currentRow[Constants.MailBox],
                    MeetingRoom =
                        currentRow[Constants.MeetingRoom] is DBNull
                            ? string.Empty
                            : (string)currentRow[Constants.MeetingRoom]
                };

                //Add the entity value into the list
                _locationInfo.Add(entity);
            }
        }

        #endregion
    }
}
