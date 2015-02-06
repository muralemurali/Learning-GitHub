using System;
using System.Collections.Generic;
using LGH.Model.IRepositories;
using LGH.Model.Entities;
using LGH.Model.ListDataSource;
using System.Configuration;
using System.Data;

namespace LGH.Data.Repositories
{
    public class FaqRepository : IFaqRepository
    {
        #region Variable Declaration

        private List<LGHFAQItems> _faqInfo;

        #endregion

        #region Constant Declaration

        private const string FaqQuestion = "Question";
        private const string FaqAnswer = "Answer";

        #endregion

        #region Public Methods

        ///// <summary>
        ///// Get Faq Info from SharePoint List
        ///// </summary>
        ///// <returns></returns>
        public IList<LGHFAQItems> GetFaqInfoList()
        {
            _faqInfo = new List<LGHFAQItems>();

            //Get Faq List Url from web.config file
            //Ex. <add key="FaqList" value="http://servername/Lists/Faq/AllItems.aspx" />
           // var listUrl = ConfigurationManager.AppSettings["FaqList"];

            var listUrl = ListConfig.ListName.FAQ;


            //Initialize the object of Listdatasource class and verify the site and list is valid for the given Url
            var list = new ListDataSource(listUrl);

            //CAML Query for getting item from List
            const string query = "<Where>" +
                                    "<IsNotNull>" +
                                        "<FieldRef Name='Question' />" +
                                    "</IsNotNull>" +
                                "</Where>"+
                                "<OrderBy>" +
                                    "<FieldRef Name='Modified' Ascending='FALSE' />" +
                                 "</OderBy>";

            const string viewFields =
                "<FieldRef Name='" + Constants.Constants.ItemId + "' />" +
                "<FieldRef Name='" + Constants.Constants.Question + "' /><FieldRef Name='" +
                Constants.Constants.Answer + "' /><FieldRef Name='" + Constants.Constants.Modified + "' />";

            //Set the attribute to 'Recursive' to get data from inside folder also
            //const string viewAttributes = "Scope='Recursive'";
            var viewAttributes = string.Empty;

            const int rowLimit = 2;

            //Get the item from list based on the CAML query
            var faqsTable = list.QueryList(query, viewFields, viewAttributes, rowLimit);

            //Dispose the objects (SPWeb and SPSite)
            list.Dispose();

            //If faqs table row count is <= 0, then return empty object
            if (faqsTable.Rows.Count <= 0) return _faqInfo;

            //If faqs table row count is > 0, then construct entity from table
            ConstructFaq(faqsTable);

            return _faqInfo;
        }

        ///// <summary>
        ///// Get All Faq Info from SharePoint List
        ///// </summary>
        ///// <returns></returns>
        public IList<LGHFAQItems> GetAllFaqInfoList()
        {
            _faqInfo = new List<LGHFAQItems>();

            //Get Faq List Url from web.config file
            //Ex. <add key="FaqList" value="http://servername/Lists/Faq/AllItems.aspx" />
            //var listUrl = ConfigurationManager.AppSettings["FaqList"];
            var listUrl = ListConfig.ListName.FAQ;

            //Initialize the object of Listdatasource class and verify the site and list is valid for the given Url
            var list = new ListDataSource(listUrl);

            //CAML Query for getting item from List
            const string query = "<Where>" +
                                    "<IsNotNull>" +
                                        "<FieldRef Name='Question' />" +
                                    "</IsNotNull>" +
                                "</Where>" +
                                "<OrderBy>" +
                                    "<FieldRef Name='Modified' Ascending='FALSE' />" +
                                 "</OderBy>";

            const string viewFields =
                "<FieldRef Name='" + Constants.Constants.ItemId + "' />" +
                "<FieldRef Name='" + Constants.Constants.Question + "' /><FieldRef Name='" +
                Constants.Constants.Answer + "' /><FieldRef Name='" + Constants.Constants.Modified + "' />";

            //Set the attribute to 'Recursive' to get data from inside folder also
            //const string viewAttributes = "Scope='Recursive'";
            var viewAttributes = string.Empty;

            //Get the item from list based on the CAML query
            var faqsTable = list.QueryList(query, viewFields, viewAttributes, -1);

            //Dispose the objects (SPWeb and SPSite)
            list.Dispose();

            //If faqs table row count is <= 0, then return empty object
            if (faqsTable.Rows.Count <= 0) return _faqInfo;

            //If faqs table row count is > 0, then construct entity from table
            ConstructFaq(faqsTable);

            return _faqInfo;
        }

        /// <summary>
        /// Add Faq Details Into SharePoint List
        /// </summary>
        /// <param name="faqEntity"></param>
        public void AddFaqItems(LGHFAQItems faqEntity)
        {
            ListDataSource targetList = null;

            //Get Upcoming Events List Url from web.config file
           // var listUrl = ConfigurationManager.AppSettings["FaqList"];

            var listUrl = ListConfig.ListName.FAQ;

            try
            {
                //Initialize the object of Listdatasource class and verify the site and list is valid for the given Url
                targetList = new ListDataSource(listUrl);

                var listFieldNames = new List<string> { FaqQuestion, FaqAnswer };
                var listFieldTypes = new List<string> { "string", "string" };
                var listFieldValues = new Dictionary<string, string>
                {
                   { FaqQuestion, faqEntity.Question },
                   { FaqAnswer, faqEntity.Answer }
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

        public IList<LGHFAQItems> GetFAQInfoListById(long itemId)
        {
            _faqInfo = new List<LGHFAQItems>();

            //Get Sponsorship List Url from web.config file
            //Ex. <add key="SponsorshipList" value="http://servername/Lists/Sponsorship%20Offerings/AllItems.aspx" />
           // var listUrl = ConfigurationManager.AppSettings["FaqList"];
            var listUrl = ListConfig.ListName.FAQ;

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
               "<FieldRef Name='" + Constants.Constants.Question + "' />" +
               "<FieldRef Name='" + Constants.Constants.Answer + "' />" +
                 "<FieldRef Name='" + Constants.Constants.Modified + "' />";
             

            //Set the attribute to 'Recursive' to get data from inside folder also
            //const string viewAttributes = "Scope='Recursive'";
            var viewAttributes = string.Empty;

            //Get the item from list based on the CAML query
            var sponsorshipTable = list.QueryList(query, viewFields, viewAttributes, -1);

            //Dispose the objects (SPWeb and SPSite)
            list.Dispose();

            //If sponsorship table row count is <= 0, then return empty object
            if (sponsorshipTable.Rows.Count <= 0) return _faqInfo;

            //If sponsorship table row count is > 0, then construct entity from table
            ConstructFaq(sponsorshipTable);

            return _faqInfo;
        }

        public string UpdateFAQItems(LGHFAQItems FAQEntity, long itemId)
        {
            ListDataSource targetList = null;
            var result = string.Empty;

            //Get Sponsorship List Url from web.config file
          //  var listUrl = ConfigurationManager.AppSettings["FaqList"];

            var listUrl = ListConfig.ListName.FAQ;

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

                var listFieldNames = new List<string> { FaqQuestion, FaqAnswer };
                var listFieldTypes = new List<string> { "string", "string"};
                var listFieldValues = new Dictionary<string, string>
                {
                    { FaqQuestion, FAQEntity.Question },
                    { FaqAnswer, FAQEntity.Answer }
                    
                    
                };

                result = targetList.UpdateListItem(query, listFieldNames, listFieldValues, listFieldTypes);
            }
            finally
            {
                if (targetList != null) targetList.Dispose();
            }
            return result;
        }


        //public void DeleteFAQItems(int ItemID)
        //{
        //    ListDataSource targetList = null;

        //    //Get Upcoming Events List Url from web.config file
        //    var listUrl = ConfigurationManager.AppSettings["FaqList"];

        //    try
        //    {
        //        //Initialize the object of Listdatasource class and verify the site and list is valid for the given Url
        //        targetList = new ListDataSource(listUrl);

               

        //        //Add the items into SharePoint List
        //        targetList.DeleteListItem(ItemID);
        //    }
        //    finally
        //    {
        //        //If object is not null then dispose the object
        //        if (targetList != null) targetList.Dispose();
        //    }
        //}
         

        #region Private Methods

        /// <summary>
        /// Add resultset table into entity
        /// </summary>
        /// <param name="faqsTable"></param>
        private void ConstructFaq(DataTable faqsTable)
        {
            for (var i = 0; i < faqsTable.Rows.Count; i++)
            {
                //Set the current Row
                var currentRow = faqsTable.Rows[i];

                //Initializ the object and assign the value into entity field
                var entity = new LGHFAQItems
                {
                    ItemId =
                        currentRow[Constants.Constants.ItemId] is DBNull
                            ? 0
                            : Convert.ToInt64(currentRow[Constants.Constants.ItemId]),
                    Question =
                        currentRow[Constants.Constants.Question] is DBNull
                            ? string.Empty
                            : (string)currentRow[Constants.Constants.Question],
                    Answer =
                        currentRow[Constants.Constants.Answer] is DBNull
                            ? string.Empty
                            : (string)currentRow[Constants.Constants.Answer],
                    Modified =
                        currentRow[Constants.Constants.Modified] is DBNull
                            ? null
                            : (DateTime?)currentRow[Constants.Constants.Modified],
                };

                //Add the entity value into the list
                _faqInfo.Add(entity);
            }
        }

        #endregion
    }
}
