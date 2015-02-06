using System;
using System.Collections.Generic;
using LGH.Model.Entities;
using LGH.Model.IRepositories;
using LGH.Model.ListDataSource;
using System.Configuration;
using System.Data;

namespace LGH.Data.Repositories
{
    public class ReportsRepository : IReportsRepository
    {

        #region Variable Declaration

        private List<LGHReports>_ReportsInfo;

        #endregion

        #region Constant Declaration
        //private const string CarouselTitle = "Title";
        private const string shortNotes = "Comments";
        //private const string ThumbnailImage = "CarouselImage";
        private const string LinkUrl = "URL";
        #endregion

        #region Public Methods

        ///// <summary>
        ///// Get Carousel Info from SharePoint List
        ///// </summary>
        ///// <returns></returns>
        public IList<LGHReports> GetReportsInfoList()
        {
            _ReportsInfo = new List<LGHReports>();

            //Get Carousel List Url from web.config file
            //Ex. <add key="CarouselList" value="http://servername/Lists/Carousel/AllItems.aspx" />
            // var listUrl = ConfigurationManager.AppSettings["CarouselList"];
            var listUrl = ListConfig.ListName.Reportslist;

            //Initialize the object of Listdatasource class and verify the site and list is valid for the given Url
            var list = new ListDataSource(listUrl);

            //CAML Query for getting item from List
            const string query = "<Where>" +
                                    "<IsNotNull>" +
                                        "<FieldRef Name='Title' />" +
                                    "</IsNotNull>" +
                                 "</Where>";

            const string viewFields =
                "<FieldRef Name='" + Constants.Constants.ItemId + "' />" +
                "<FieldRef Name='" + Constants.Constants.ReportsLinkUrl + "' />" +
                "<FieldRef Name='" + Constants.Constants.shortNotes;

            //Set the attribute to 'Recursive' to get data from inside folder also
            //const string viewAttributes = "Scope='Recursive'";
            var viewAttributes = string.Empty;

            //Get the item from list based on the CAML query
            var carouselTable = list.QueryList(query, viewFields, viewAttributes, -1);

            //Dispose the objects (SPWeb and SPSite)
            list.Dispose();

            //If carousel table row count is <= 0, then return empty object
            if (carouselTable.Rows.Count <= 0) return _ReportsInfo;

            //If carousel table row count is > 0, then construct entity from table
            ConstructCarousel(carouselTable);

            return _ReportsInfo;
        }
        /// <summary>
        /// get Carousel Image Details by id from SharePoint List
        /// </summary>
        /// <param name="euOfferingsEntity"></param>
        public IList<LGHReports> GetReportsInfosListById(long itemId)
        {
            _ReportsInfo = new List<LGHReports>();

            //Get Sponsorship List Url from web.config file
            //Ex. <add key="SponsorshipList" value="http://servername/Lists/Sponsorship%20Offerings/AllItems.aspx" />
            // var listUrl = ConfigurationManager.AppSettings["CarouselList"];
            var listUrl = ListConfig.ListName.Reportslist;

            //Initialize the object of Listdatasource class and verify the site and list is valid for the given Url
            var list = new ListDataSource(listUrl);

            //CAML Query for getting item from List
            var query = "<Where>" +
                            "<Eq>" +
                                "<FieldRef Name='ID' />" +
                                "<Value Type='Integer'>" + itemId.ToString() + "</Value>" +
                            "</Eq>" +
                         "</Where>";

            string viewFields =
               "<FieldRef Name='" + Constants.Constants.ItemId + "' />" +
                "<FieldRef Name='" + Constants.Constants.ReportsLinkUrl + "' />" +
               "<FieldRef Name='" + Constants.Constants.shortNotes + "' />";

            //Set the attribute to 'Recursive' to get data from inside folder also
            //const string viewAttributes = "Scope='Recursive'";
            var viewAttributes = string.Empty;

            //Get the item from list based on the CAML query
            var sponsorshipTable = list.QueryList(query, viewFields, viewAttributes, -1);

            //Dispose the objects (SPWeb and SPSite)
            list.Dispose();

            //If sponsorship table row count is <= 0, then return empty object
            if (sponsorshipTable.Rows.Count <= 0) return _ReportsInfo;

            //If sponsorship table row count is > 0, then construct entity from table
            ConstructCarousel(sponsorshipTable);

            return _ReportsInfo;
        }
        /// <summary>
        /// get Carousel Image Details from SharePoint List
        /// </summary>
        /// <param name="euOfferingsEntity"></param>

        public IList<LGHReports> GetAllReportsInfoList()
        {
            _ReportsInfo = new List<LGHReports>();

            //Get SS EU Offerings List Url from web.config file
            //Ex. <add key="EuOfferingsList" value="http://servername/Lists/SS%20EU%20Offerings/AllItems.aspx" />
            // var listUrl = ConfigurationManager.AppSettings["CarouselList"];
            var listUrl = ListConfig.ListName.Reportslist;

            //Initialize the object of Listdatasource class and verify the site and list is valid for the given Url
            var list = new ListDataSource(listUrl);

            //CAML Query for getting item from List
            const string query = "<OrderBy>" +
                                    "<FieldRef Name='Modified' Ascending='FALSE' />" +
                                 "</OrderBy>";

            const string viewFields =
                "<FieldRef Name='" + Constants.Constants.ItemId + "' />" +
                "<FieldRef Name='" + Constants.Constants.ReportsLinkUrl + "' />" +
                "<FieldRef Name='" + Constants.Constants.shortNotes + "' />";

            //Set the attribute to 'Recursive' to get data from inside folder also
            //const string viewAttributes = "Scope='Recursive'";
            var viewAttributes = string.Empty;



            //Get the item from list based on the CAML query
            var euOfferingsTable = list.QueryList(query, viewFields, viewAttributes, -1);

            //Dispose the objects (SPWeb and SPSite)
            list.Dispose();

            //If euOfferings table row count is <= 0, then return empty object
            if (euOfferingsTable.Rows.Count <= 0) return _ReportsInfo;

            //If euOfferings table row count is > 0, then construct entity from table
            ConstructCarousel(euOfferingsTable);

            return _ReportsInfo;
        }
        /// <summary>
        /// update Carousel Image Details in SharePoint List
        /// </summary>
        /// <param name="euOfferingsEntity"></param>
        public string UpdateReportsItems(LGHReports ReportsEntity, long itemId)
        {
            ListDataSource targetList = null;
            var result = string.Empty;

            //Get Sponsorship List Url from web.config file
            // var listUrl = ConfigurationManager.AppSettings["CarouselList"];
            var listUrl = ListConfig.ListName.Reportslist;

            try
            {
                //Initialize the object of Listdatasource class and verify the site and list is valid for the given Url
                targetList = new ListDataSource(listUrl);

                //CAML Query for getting item from List
                var query = "<Where>" +
                                "<Eq>" +
                                    "<FieldRef Name='ID' />" +
                                    "<Value Type='Integer'>" + itemId.ToString() + "</Value>" +
                                "</Eq>" +
                             "</Where>";

                var listFieldNames = new List<string> {  shortNotes, LinkUrl };
                var listFieldTypes = new List<string> { "string", "HyperLinkField" };
                var listFieldValues = new Dictionary<string, string>
                {
                   
                    { shortNotes,ReportsEntity.shortNotes  },
                    {LinkUrl,ReportsEntity.Url}
                    
                };

                result = targetList.UpdateListItem(query, listFieldNames, listFieldValues, listFieldTypes);
            }
            finally
            {
                if (targetList != null) targetList.Dispose();
            }
            return result;
        }
        /// <summary>
        /// Add Carousel Image Details Into SharePoint List
        /// </summary>
        /// <param name="euOfferingsEntity"></param>
        public void AddReportsItems(LGHReports ReportsEntity)
        {
            ListDataSource targetList = null;

            //Get Upcoming Events List Url from web.config file
            // var listUrl = ConfigurationManager.AppSettings["CarouselList"];
            var listUrl = ListConfig.ListName.Reportslist;

            try
            {
                //Initialize the object of Listdatasource class and verify the site and list is valid for the given Url
                targetList = new ListDataSource(listUrl);

                var listFieldNames = new List<string> {  shortNotes,  LinkUrl };
                var listFieldTypes = new List<string> { "string","HyperLinkField" };
                var listFieldValues = new Dictionary<string, string>
                {
                  
                   { shortNotes, ReportsEntity.shortNotes },
                  
                   {LinkUrl,ReportsEntity.Url}
                };

                //Add the items into SharePoint List
                targetList.AddListItem(listFieldNames, listFieldValues, listFieldTypes);
            }
            finally
            {
                //If object is not null then dispose the object
                if (targetList != null) targetList.Dispose();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Add resultset table into entity
        /// </summary>
        /// <param name="carouselTable"></param>
        private void ConstructCarousel(DataTable carouselTable)
        {
            for (var i = 0; i < carouselTable.Rows.Count; i++)
            {
                //Set the current Row
                var currentRow = carouselTable.Rows[i];

                //Initializ the object and assign the value into entity field
                var entity = new LGHReports
                {
                    ItemId =
                        currentRow[Constants.Constants.ItemId] is DBNull
                            ? 0
                            : Convert.ToInt64(currentRow[Constants.Constants.ItemId]),
                    shortNotes =
                        currentRow[Constants.Constants.shortNotes] is DBNull
                            ? string.Empty
                            : (string)currentRow[Constants.Constants.shortNotes],
                    Url =
                currentRow[Constants.Constants.ReportsLinkUrl] is DBNull
                    ? string.Empty
                    : (string)currentRow[Constants.Constants.ReportsLinkUrl],
                };

                //Add the entity value into the list
                _ReportsInfo.Add(entity);
            }
        }

        #endregion
    }
}
