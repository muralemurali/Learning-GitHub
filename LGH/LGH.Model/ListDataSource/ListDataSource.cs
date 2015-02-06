using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SharePoint;
using System.Data;
using System.Globalization;
using LGH.Model.IRepositories;
using LGH.Model.Entities;
using LGH.Model.Utility;
using Microsoft.SharePoint.Taxonomy;
using Microsoft.SharePoint.Publishing;
using Microsoft.SharePoint.Publishing.Fields;
using LGH.ListConfig;

namespace LGH.Model.ListDataSource
{
    public class ListDataSource : IDisposable 
    {
        #region Variable Declaration

        private  readonly SPSite _spSite;
        private  readonly SPWeb _spWeb;
        private readonly SPList _spList;
        private  readonly bool _listExists;

        #endregion

        #region Object Dispose

        /// <summary>
        /// Dispose the SharePoint Objects
        /// </summary>
        public void Dispose()
        {
            //Check the _spWeb object is not null. If not null and dispose the object
            if (_spWeb != null)
            {
                _spWeb.Dispose();
            }
            //Check the _spSite object is not null. If not null and dispose the object
            if (_spSite != null)
            {
                _spSite.Dispose();
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Check the list is exist in the site
        /// </summary>
        /// <param name="listUrl"></param>
        public ListDataSource(string listUrl)
        {
            try
            {



               
                //SPSecurity.RunWithElevatedPrivileges(delegate()
                //{
                //    SPSite _spSite = new SPSite(SPContext.Current.Site.Url);

                //    SPWeb _spWeb = _spSite.OpenWeb();
                        
                //    _spList = _spWeb.Lists.TryGetList(listUrl);
                        
                //        // run elevated code here
                    
                //});
                //Create new site object with the url
                _spSite = new SPSite(SPContext.Current.Site.Url);

                //Open the web using site object
                _spWeb = _spSite.OpenWeb();

                //Get the list using web object
                _spList = _spWeb.Lists.TryGetList(listUrl);

                //Check the list is exists or not. If exists then assign true
                if (_spList != null)
                {
                    _listExists = true;
                }
            }
            catch
            {
                if (string.IsNullOrEmpty(listUrl))
                {
                    throw new NullReferenceException("Url key/value field is null or empty in web.config file : " + null);
                }
                if (_spSite == null)
                {
                    throw new NullReferenceException("Site does not exists. Please check the URL of site. <br />Your site URL is : " + listUrl);
                }
                if (_spList == null)
                {
                    throw new NullReferenceException("List does not exists. Please check the List name in the URL of site. <br />Your site URL is : " + listUrl);
                }
            }
        }

        /// <summary>
        /// Get Item from List and return it as DataTable
        /// </summary>
        /// <param name="caml"></param>
        /// <param name="viewFields"></param>
        /// <param name="viewAttributes"></param>
        /// <param name="rowLimit"></param>
        /// <returns></returns>
        public DataTable QueryList(string caml, string viewFields, string viewAttributes, int rowLimit)
        {
            //Check the caml query is null or empty, if null or empty then throw error and exit the method
            if (string.IsNullOrEmpty(caml))
            {
                const string camlError = "CAML query must not be null or empty";
                throw new ArgumentNullException(camlError, "CAML");
            }
            //Check the list is exists or not, if not throw error and exit the method
            if (!_listExists)
            {
                const string listError = "Lists";
                throw new ArgumentException("List does not exists", listError);
            }

            //Create SPQuery object and assign the caml query into Query properties
            var query = new SPQuery { Query = caml };

            //Check the view fields are not null or not empty 
            //If not empty then assign the values
            if (!string.IsNullOrEmpty(viewFields))
            {
                query.ViewFields = viewFields;
                query.ViewFieldsOnly = true;
            }

            //Check the view attributes are not null or not empty 
            //If not empty then assign the values
            if (!string.IsNullOrEmpty(viewAttributes))
            {
                query.ViewAttributes = viewAttributes;
            }

            if (rowLimit != -1)
            {
                query.RowLimit = (uint)rowLimit;
            }

            //Get the items from SharePoint List and convert into DataTable
            using (var result = _spList.GetItems(query).GetDataTable())
            {
                //If the result is not null and count is greater than zero, then return the result table
                if (result != null && result.Rows.Count > 0)
                {
                    return result;
                }

                //If the result count is less than zero, then return empty table
                using (var queryTable = new DataTable { Locale = CultureInfo.CurrentCulture })
                {
                    return queryTable;
                }
            }
        }

        /// <summary>
        /// Add Items into List
        /// </summary>
        /// <param name="listFieldNames"></param>
        /// <param name="listFieldValues"></param>
        /// <param name="listFieldTypes"></param>
        public void AddListItem(List<string> listFieldNames, Dictionary<string, string> listFieldValues, List<string> listFieldTypes)
        {
            //Set Allowunsafe Updates to true for both spsite and spweb to avoid the security issue
            _spSite.AllowUnsafeUpdates = true;
            _spList.ParentWeb.AllowUnsafeUpdates = true;

            //Add new item
            var currentItem = _spList.AddItem();
            //Loop through the items and set the values to the object
            for (var i = 0; i < listFieldNames.Count; i++)
            {
                switch (listFieldTypes[i])
                {
                    case "MultiLookup":
                        {
                            string[] seperator = { ";" };
                            var valueCollection = listFieldValues[listFieldNames[i]].Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                            var lookupValueCollection = new SPFieldLookupValueCollection();
                            lookupValueCollection.AddRange(valueCollection.Select(item => new SPFieldLookupValue(item)));
                            currentItem[listFieldNames[i]] = lookupValueCollection;
                        }
                        break;
                    case "ImageField":
                        {
                            var imgFieldValue = new ImageFieldValue();
                            imgFieldValue.ImageUrl = listFieldValues[listFieldNames[i]];

                            currentItem[listFieldNames[i]] = imgFieldValue;
                        }
                        break;
                    case "HyperLinkField":
                        {
                            var linkFieldValue = new SPFieldUrlValue();
                            string[] urlValue = listFieldValues[listFieldNames[i]].Split(',') ;
                            //string[] urlValue = listFieldValues[listFieldNames[i]].Contains(",") ? listFieldValues[listFieldNames[i]].Split(',') : new string[] { "", "" };
                            linkFieldValue.Url = urlValue[1];
                            string textDescription = urlValue[0] == "" ? string.Empty : urlValue[0];
                            linkFieldValue.Description = textDescription;
                            currentItem[listFieldNames[i]] = linkFieldValue;
                        }
                        break;
                   
                    
                    default:
                        currentItem[listFieldNames[i]] = listFieldValues[listFieldNames[i]];
                        break;
                }
            }

            //Update the list item
            currentItem.Update();

            //Set Allowunsafe Updates to false for both spsite and spweb
            _spList.ParentWeb.AllowUnsafeUpdates = false;
            _spSite.AllowUnsafeUpdates = false;
        }

        /// <summary>
        /// Update Item into SharePoint List
        /// </summary>
        /// <param name="camlQuery"></param>
        /// <param name="listFieldNames"></param>
        /// <param name="listFieldValues"></param>
        /// <param name="listFieldTypes"></param>
        /// <returns></returns>
        public string UpdateListItem(string camlQuery, List<string> listFieldNames, Dictionary<string, string> listFieldValues, List<string> listFieldTypes)
        {
            try
            {
                _spSite.AllowUnsafeUpdates = true;
                _spList.ParentWeb.AllowUnsafeUpdates = true;
                var query = new SPQuery { Query = camlQuery };
                var listItem = _spList.GetItems(query)[0];

                //foreach (var listFieldName in listFieldNames)
                //{
                //    listItem[listFieldName] = listFieldValues[listFieldName];
                //}
                for (var i = 0; i < listFieldNames.Count; i++)
                {
                    switch (listFieldTypes[i])
                    {
                        case "MultiLookup":
                            {
                                string[] seperator = { ";" };
                                var valueCollection = listFieldValues[listFieldNames[i]].Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                                var lookupValueCollection = new SPFieldLookupValueCollection();
                                lookupValueCollection.AddRange(valueCollection.Select(item => new SPFieldLookupValue(item)));
                                listItem[listFieldNames[i]] = lookupValueCollection;
                            }
                            break;
                        case "ImageField":
                            {
                                var imgFieldValue = new ImageFieldValue();
                                imgFieldValue.ImageUrl = listFieldValues[listFieldNames[i]];

                                listItem[listFieldNames[i]] = imgFieldValue;
                            }
                            break;

                        case "HyperLinkField":
                            {
                                var linkFieldValue = new SPFieldUrlValue();
                                string[] urlValue = listFieldValues[listFieldNames[i]].Split(',');
                                //string[] urlValue = listFieldValues[listFieldNames[i]].Contains(",") ? listFieldValues[listFieldNames[i]].Split(',') : new string[] { "", "" };
                                linkFieldValue.Url = urlValue[1];
                                string textDescription = urlValue[0] == "" ? string.Empty : urlValue[0];
                                linkFieldValue.Description = textDescription;
                                listItem[listFieldNames[i]] = linkFieldValue;
                            }
                            break;

                        default:
                            listItem[listFieldNames[i]] = listFieldValues[listFieldNames[i]];
                            break;
                    }
                }

                listItem.Update();
                _spList.ParentWeb.AllowUnsafeUpdates = false;
                _spSite.AllowUnsafeUpdates = false;
                return "SUCCESS";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Add File Content into Library
        /// </summary>
        /// <param name="fileContent"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string AddFileStreamToLibrary(byte[] fileContent, string fileName)
        {
            try
            {
                //Set Allowunsafe Updates to true for both spsite and spweb to avoid the security issue
                _spSite.AllowUnsafeUpdates = true;
                _spList.ParentWeb.AllowUnsafeUpdates = true;

                //Upload File into Library
                _spList.RootFolder.Files.Add(_spList.RootFolder.Url + "/" + fileName, fileContent, true);

                //Update the list
                _spList.Update();

                //Set Allowunsafe Updates to true for both spsite and spweb
                _spList.ParentWeb.AllowUnsafeUpdates = false;
                _spSite.AllowUnsafeUpdates = false;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "success" + "|" + _spList.RootFolder.Url + "/" + fileName;
        }

        ///// <summary>
        ///// Get Term Store Values to Build Menu
        ///// </summary>
        ///// <param name="termSetName"></param>
        ///// <returns></returns>
        //public List<TopNavigationMenu> QueryTermstore(string termSetName)
        //{
        //    List<TopNavigationMenu> navigationList = new List<TopNavigationMenu>();

        //    TaxonomySession taxonomySession = new TaxonomySession(_spSite);
        //    TermStore taxStore = taxonomySession.TermStores[0];
        //    Group taxGroup = taxStore.GetSiteCollectionGroup(_spSite);
        //    TermSet termSet = taxGroup.TermSets[termSetName];

        //    if (termSet != null)
        //    {
        //        foreach (Term term in termSet.Terms)
        //        {
        //            if (term.Terms.Count >= 0)
        //            {
        //                var entity = new TopNavigationMenu
        //                {
        //                    MenuTitle = term.Name
        //                };

        //                navigationList.Add(entity);
        //            }
        //        }
        //    }

        //    return navigationList;
        //}

        /// <summary>
        /// Get Term Store Values to Build Menu
        /// </summary>
        /// <param name="termSetName"></param>
        /// <returns></returns>
        public string QueryTermstore(string termSetName)
        {
            var htmlBuilder = string.Empty;

            TaxonomySession taxonomySession = new TaxonomySession(_spSite);
            TermStore taxStore = taxonomySession.TermStores[0];
            Group taxGroup = taxStore.GetSiteCollectionGroup(_spSite);
            TermSet termSet = taxGroup.TermSets[termSetName];
            int count = 0;
            if (termSet != null)
            {
                foreach (Term term in termSet.Terms)
                {
                    count = count + 1;
                    if (term.Terms.Count >= 0)
                    {
                        htmlBuilder += ConstructMenu(term, count);
                    }
                }
            }

            return htmlBuilder;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Consturct Term Store Item as Menu
        /// </summary>
        /// <param name="termname"></param>
        /// <param name="TermCount"></param>
        /// <returns></returns>
        private string ConstructMenu(Term termname, int TermCount)
        {

            string htmlBuilder = string.Empty;

            string ParentTermImageUrl = string.Empty;
            string ParentTermUrl = string.Empty;
            string ChildTermUrl = string.Empty;
            string HtmlBuilder = string.Empty;

            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                ParentTermImageUrl = (termname.CustomProperties.ContainsKey("ImageUrl") ? termname.CustomProperties["ImageUrl"] : string.Empty);
                ParentTermUrl = (termname.CustomProperties.ContainsKey("Url") ? termname.CustomProperties["Url"] : string.Empty);



                if (termname.Terms.Count > 0)
                {

                    double subTermsCount = termname.Terms.Count / 5;
                    double DivAlloc = Math.Ceiling(subTermsCount);
                    int totalcount = (int)DivAlloc;
                    int classcount = 0;

                    if (termname.Terms.Count % 5 == 0)
                    {
                        classcount = totalcount + 2;
                    }
                    else
                    {
                        classcount = totalcount + 3;
                    }


                    string className = "dropdown_" + classcount + "columns";


                    if (TermCount > 2)
                    {
                        htmlBuilder += @"<li><a href='#' class='drop'>" + termname.Name + @"</a><!-- Begin 5 columns-->
        		                                <div class='" + className + " align_right dbbg'>";

                    }

                    else
                    {
                        htmlBuilder += @"<li><a href='#' class='drop'>" + termname.Name + @"</a><!-- Begin 2 columns -->
                                                <div class='" + className + " dbbg'> ";


                    }
                    htmlBuilder += @" <div class='col_2'>
                                                       <img src='" + ParentTermImageUrl + @"' alt='dropmenu' />  
                                                    </div>";
                    int count = 0;

                    htmlBuilder += @" <div class='col_1'> <ul>";

                    foreach (Term drpTerm in termname.Terms)
                    {
                        count += 1;
                        ChildTermUrl = (drpTerm.CustomProperties.ContainsKey("Url") ? drpTerm.CustomProperties["Url"] : string.Empty);
                        if (count < 5)
                        {
                            htmlBuilder += "  <li><a href='" + ChildTermUrl + "'>" + drpTerm.Name + "</a></li>";
                        }
                        else if (count % 5 == 0)
                        {
                            htmlBuilder += "  <li><a href='" + ChildTermUrl + "'>" + drpTerm.Name + "</a></li></ul></div>";
                            htmlBuilder += "<div class='col_1'> <ul>";

                        }
                        else
                        {
                            htmlBuilder += "  <li><a href='" + ChildTermUrl + "' >" + drpTerm.Name + "</a></li>";
                        }


                    }

                    htmlBuilder += "</div></li>";

                }

                else
                {

                    htmlBuilder += @" <li><a href='" + ParentTermUrl + "'>" + termname.Name + "</a></li>";
                }
            });
            return htmlBuilder;
        }

        public void DeleteListItem(int ItemIdDelete,string listName)
        {
            
            SPListItem itemToDelete = null;
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite eSite = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb eWeb = eSite.OpenWeb())
                    {
                        eWeb.AllowUnsafeUpdates = true;
                        SPList list = eWeb.Lists.TryGetList(listName);
                        if (list != null)
                        {
                             itemToDelete = list.GetItemById(ItemIdDelete);
                             itemToDelete.Delete();
                        }

                        list.Update();
                        eWeb.AllowUnsafeUpdates = false;
                    }
                }
            });

        }



        #endregion
    }
}
