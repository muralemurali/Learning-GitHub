using System;
using System.Collections.Generic;
using LGH.Model.Entities;
using LGH.Model.IRepositories;
using LGH.Model.ListDataSource;
using System.Configuration;
using System.Data;

namespace LGH.Data.Repositories
{
    public class CarouselRepository : ICarouselRepository
    {
        #region Variable Declaration

        private List<CarouselHomePage> _carouselInfo;

        #endregion

        #region Constant Declaration
        private const string CarouselTitle = "Title";
        private const string Description = "TileDescription";
        private const string ThumbnailImage = "CarouselImage";
        private const string CarouselUrl = "LinkUrl";
        #endregion

        #region Public Methods

        ///// <summary>
        ///// Get Carousel Info from SharePoint List
        ///// </summary>
        ///// <returns></returns>
        public IList<CarouselHomePage> GetCarouselInfoList()
        {
            _carouselInfo = new List<CarouselHomePage>();

            //Get Carousel List Url from web.config file
            //Ex. <add key="CarouselList" value="http://servername/Lists/Carousel/AllItems.aspx" />
           // var listUrl = ConfigurationManager.AppSettings["CarouselList"];
            var listUrl = ListConfig.ListName.CarouselList;

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
                "<FieldRef Name='" + Constants.Constants.Url + "' />"+
                "<FieldRef Name='" + Constants.Constants.Title + "' /><FieldRef Name='" +
                Constants.Constants.Description + "' /><FieldRef Name='" + Constants.Constants.Image +
                "' />";

            //Set the attribute to 'Recursive' to get data from inside folder also
            //const string viewAttributes = "Scope='Recursive'";
            var viewAttributes = string.Empty;

            //Get the item from list based on the CAML query
            var carouselTable = list.QueryList(query, viewFields, viewAttributes, -1);

            //Dispose the objects (SPWeb and SPSite)
            list.Dispose();

            //If carousel table row count is <= 0, then return empty object
            if (carouselTable.Rows.Count <= 0) return _carouselInfo;

            //If carousel table row count is > 0, then construct entity from table
            ConstructCarousel(carouselTable);

            return _carouselInfo;
        }
        /// <summary>
        /// get Carousel Image Details by id from SharePoint List
        /// </summary>
        /// <param name="euOfferingsEntity"></param>
        public IList<CarouselHomePage> GetCarouselInfosListById(long itemId)
        {
            _carouselInfo = new List<CarouselHomePage>();

            //Get Sponsorship List Url from web.config file
            //Ex. <add key="SponsorshipList" value="http://servername/Lists/Sponsorship%20Offerings/AllItems.aspx" />
           // var listUrl = ConfigurationManager.AppSettings["CarouselList"];
            var listUrl = ListConfig.ListName.CarouselList;

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
                "<FieldRef Name='" + Constants.Constants.Url + "' />" +
               "<FieldRef Name='" + Constants.Constants.Title + "' />" +
               "<FieldRef Name='" + Constants.Constants.Description + "' />" +
               "<FieldRef Name='" + Constants.Constants.Image + "' />";


            //Set the attribute to 'Recursive' to get data from inside folder also
            //const string viewAttributes = "Scope='Recursive'";
            var viewAttributes = string.Empty;

            //Get the item from list based on the CAML query
            var sponsorshipTable = list.QueryList(query, viewFields, viewAttributes, -1);

            //Dispose the objects (SPWeb and SPSite)
            list.Dispose();

            //If sponsorship table row count is <= 0, then return empty object
            if (sponsorshipTable.Rows.Count <= 0) return _carouselInfo;

            //If sponsorship table row count is > 0, then construct entity from table
            ConstructCarousel(sponsorshipTable);

            return _carouselInfo;
        }
        /// <summary>
        /// get Carousel Image Details from SharePoint List
        /// </summary>
        /// <param name="euOfferingsEntity"></param>

        public IList<CarouselHomePage> GetAllCarouselInfoList()
        {
            _carouselInfo = new List<CarouselHomePage>();

            //Get SS EU Offerings List Url from web.config file
            //Ex. <add key="EuOfferingsList" value="http://servername/Lists/SS%20EU%20Offerings/AllItems.aspx" />
           // var listUrl = ConfigurationManager.AppSettings["CarouselList"];
            var listUrl = ListConfig.ListName.CarouselList;

            //Initialize the object of Listdatasource class and verify the site and list is valid for the given Url
            var list = new ListDataSource(listUrl);

            //CAML Query for getting item from List
            const string query = "<Where>" +
                                    "<IsNotNull>" +
                                        "<FieldRef Name='Title' />" +
                                    "</IsNotNull>" +
                                "</Where>" +
                                "<OrderBy>" +
                                    "<FieldRef Name='Modified' Ascending='FALSE' />" +
                                 "</OderBy>";

            const string viewFields =
                "<FieldRef Name='" + Constants.Constants.ItemId + "' />" +
                "<FieldRef Name='" + Constants.Constants.Url + "' />" +
                "<FieldRef Name='" + Constants.Constants.Title + "' /><FieldRef Name='" +
                Constants.Constants.Description + "' /><FieldRef Name='" + Constants.Constants.Image +
                "' />";

            //Set the attribute to 'Recursive' to get data from inside folder also
            //const string viewAttributes = "Scope='Recursive'";
            var viewAttributes = string.Empty;



            //Get the item from list based on the CAML query
            var euOfferingsTable = list.QueryList(query, viewFields, viewAttributes, -1);

            //Dispose the objects (SPWeb and SPSite)
            list.Dispose();

            //If euOfferings table row count is <= 0, then return empty object
            if (euOfferingsTable.Rows.Count <= 0) return _carouselInfo;

            //If euOfferings table row count is > 0, then construct entity from table
            ConstructCarousel(euOfferingsTable);

            return _carouselInfo;
        }
        /// <summary>
        /// update Carousel Image Details in SharePoint List
        /// </summary>
        /// <param name="euOfferingsEntity"></param>
        public string UpdateCarouselItems(CarouselHomePage carouselEntity, long itemId)
        {
            ListDataSource targetList = null;
            var result = string.Empty;

            //Get Sponsorship List Url from web.config file
           // var listUrl = ConfigurationManager.AppSettings["CarouselList"];
            var listUrl = ListConfig.ListName.CarouselList;

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

                var listFieldNames = new List<string> { CarouselTitle, Description, ThumbnailImage,CarouselUrl };
                var listFieldTypes = new List<string> { "string", "string", "ImageField","string" };
                var listFieldValues = new Dictionary<string, string>
                {
                    { CarouselTitle, carouselEntity.CarouselTitle },
                    { Description, carouselEntity.CarouselDescription },
                    { ThumbnailImage, carouselEntity.CarouselImage },
                    {CarouselUrl,carouselEntity.CarouselUrl}
                    
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
        public void AddCarouselItems(CarouselHomePage CarouselEntity)
        {
            ListDataSource targetList = null;

            //Get Upcoming Events List Url from web.config file
           // var listUrl = ConfigurationManager.AppSettings["CarouselList"];
            var listUrl = ListConfig.ListName.CarouselList;

            try
            {
                //Initialize the object of Listdatasource class and verify the site and list is valid for the given Url
                targetList = new ListDataSource(listUrl);

                var listFieldNames = new List<string> { CarouselTitle, Description, ThumbnailImage, CarouselUrl };
                var listFieldTypes = new List<string> { "string", "string", "ImageField","string" };
                var listFieldValues = new Dictionary<string, string>
                {
                   { CarouselTitle, CarouselEntity.CarouselTitle },
                   { Description, CarouselEntity.CarouselDescription },
                   { ThumbnailImage, CarouselEntity.CarouselImage },
                   {CarouselUrl,CarouselEntity.CarouselUrl}
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

        public string AddCarouselFileStreamToLibrary(byte[] fileContent, string fileName)
        {
            ListDataSource targetList = null;

            //Get Event Form Image Library Url from web.config file
          //  var listUrl = ConfigurationManager.AppSettings["ImageLibraryList"];
            var listUrl = ListConfig.ListName.LGHImageLibrary;

            try
            {
                //Initialize the object of Listdatasource class and verify the site and list is valid for the given Url
                targetList = new ListDataSource(listUrl);

                //Call Add File Stream To Library method to upload the file
                return targetList.AddFileStreamToLibrary(fileContent, fileName);
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
                var entity = new CarouselHomePage
                {
                    ItemId =
                        currentRow[Constants.Constants.ItemId] is DBNull
                            ? 0
                            : Convert.ToInt64(currentRow[Constants.Constants.ItemId]),
                    CarouselTitle =
                        currentRow[Constants.Constants.Title] is DBNull
                            ? string.Empty
                            : (string)currentRow[Constants.Constants.Title],
                    CarouselDescription =
                        currentRow[Constants.Constants.Description] is DBNull
                            ? string.Empty
                            : (string)currentRow[Constants.Constants.Description],
                    CarouselImage =
                        currentRow[Constants.Constants.Image] is DBNull
                            ? string.Empty
                            : (string)currentRow[Constants.Constants.Image.Replace("\"", "\'")],
                    CarouselUrl =
                currentRow[Constants.Constants.Url] is DBNull
                    ? string.Empty
                    : (string)currentRow[Constants.Constants.Url],
                };

                //Add the entity value into the list
                _carouselInfo.Add(entity);
            }
        }

        #endregion
    }
}
