using System;
using System.Collections.Generic;
using LGH.Model.Entities;
using LGH.Model.IRepositories;
using LGH.Model.ListDataSource;
using System.Configuration;
using System.Data;


namespace LGH.Data.Repositories
{
    public class NewsRepository:INewsRepository
    {

        #region Variable Declaration

        private List<LGHNews> _newsInfo;

        #endregion

        #region Constant Declaration
        //private const string CarouselTitle = "Title";
        private const string shortNotes = "Comments";
        //private const string ThumbnailImage = "CarouselImage";
        private const string LinkUrl = "URL";
        #endregion

        public IList<LGHNews> GetNewsInfoList()
        {
            _newsInfo = new List<LGHNews>();

            //Get Carousel List Url from web.config file
            //Ex. <add key="CarouselList" value="http://servername/Lists/Carousel/AllItems.aspx" />
            // var listUrl = ConfigurationManager.AppSettings["CarouselList"];
            var listUrl = ListConfig.ListName.NewsList;

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
            if (carouselTable.Rows.Count <= 0) return _newsInfo;

            //If carousel table row count is > 0, then construct entity from table
            ConstructCarousel(carouselTable);

            return _newsInfo;
        }

        public void AddNewslItems(LGHNews NewsEntity)
        {
            ListDataSource targetList = null;

            //Get Upcoming Events List Url from web.config file
            // var listUrl = ConfigurationManager.AppSettings["CarouselList"];
            var listUrl = ListConfig.ListName.NewsList;

            try
            {
                //Initialize the object of Listdatasource class and verify the site and list is valid for the given Url
                targetList = new ListDataSource(listUrl);

                var listFieldNames = new List<string> { shortNotes, LinkUrl };
                var listFieldTypes = new List<string> { "string", "HyperLinkField" };
                var listFieldValues = new Dictionary<string, string>
                {
                  
                   { shortNotes, NewsEntity.shortNotes },
                  
                   {LinkUrl,NewsEntity.Url}
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

        public IList<LGHNews> GetNewsInfosListById(long itemId)
        {
            _newsInfo = new List<LGHNews>();

            //Get Sponsorship List Url from web.config file
            //Ex. <add key="SponsorshipList" value="http://servername/Lists/Sponsorship%20Offerings/AllItems.aspx" />
            // var listUrl = ConfigurationManager.AppSettings["CarouselList"];
            var listUrl = ListConfig.ListName.NewsList;

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
            if (sponsorshipTable.Rows.Count <= 0) return _newsInfo;

            //If sponsorship table row count is > 0, then construct entity from table
            ConstructCarousel(sponsorshipTable);

            return _newsInfo;
        }

        public IList<LGHNews> GetAllNewsInfoList()
        {
            _newsInfo = new List<LGHNews>();

            //Get SS EU Offerings List Url from web.config file
            //Ex. <add key="EuOfferingsList" value="http://servername/Lists/SS%20EU%20Offerings/AllItems.aspx" />
            // var listUrl = ConfigurationManager.AppSettings["CarouselList"];
            var listUrl = ListConfig.ListName.NewsList;

            //Initialize the object of Listdatasource class and verify the site and list is valid for the given Url
            var list = new ListDataSource(listUrl);

            //CAML Query for getting item from List
            const string query = "<OrderBy>" +
                                    "<FieldRef Name='Modified' Ascending='FALSE' />" +
                                 "</OderBy>";

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
            if (euOfferingsTable.Rows.Count <= 0) return _newsInfo;

            //If euOfferings table row count is > 0, then construct entity from table
            ConstructCarousel(euOfferingsTable);

            return _newsInfo;
        }

        public string UpdateNewsItems(LGHNews NewsEntity, long itemId)
        {
            ListDataSource targetList = null;
            var result = string.Empty;

            //Get Sponsorship List Url from web.config file
            // var listUrl = ConfigurationManager.AppSettings["CarouselList"];
            var listUrl = ListConfig.ListName.NewsList;

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

                var listFieldNames = new List<string> { shortNotes, LinkUrl };
                var listFieldTypes = new List<string> { "string", "HyperLinkField" };
                var listFieldValues = new Dictionary<string, string>
                {
                   
                    { shortNotes,NewsEntity.shortNotes  },
                    {LinkUrl,NewsEntity.Url}
                    
                };

                result = targetList.UpdateListItem(query, listFieldNames, listFieldValues, listFieldTypes);
            }
            finally
            {
                if (targetList != null) targetList.Dispose();
            }
            return result;
        }

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
                var entity = new LGHNews
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
                _newsInfo.Add(entity);
            }
        }

        #endregion
    }
}
