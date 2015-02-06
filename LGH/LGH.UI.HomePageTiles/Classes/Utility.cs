using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Taxonomy;


namespace LGH.UI.HomePageTiles.Classes
{
    public class Utility
    {

        public static TermSet GetTermSet(SPSite Osite,string termsetName)
        {

            TermSet LGHTermset = null;
            try
            {
              TaxonomySession TaxonomySession = new TaxonomySession(Osite);
              TermStore TaxStore = TaxonomySession.TermStores[0];
              Group TaxGroup = TaxStore.GetSiteCollectionGroup(Osite);
              LGHTermset = TaxGroup.TermSets[termsetName];
            }
            catch (Exception e)
            {
            }
            return LGHTermset;
        }

        public static string GetLIDetails(TermSet termName)
        {
            string HtmlBuilder = string.Empty;
            try
            {
               

                int count = 0;
                if (termName != null)
                {
                    foreach (Term term in termName.Terms)
                    {
                        if (term.Terms.Count >= 0)
                        {
                            count += 1;
                            HtmlBuilder += LIConstruction(term);

                        }
                    }

                }
            }
            catch (Exception e)
            {
            }

            return HtmlBuilder;
        }
        public static string LIConstruction(Term termname)
        {

            string htmlBuilder = string.Empty;
            string ParentTermUrl = string.Empty;
            string ChildTermUrl = string.Empty;
            ParentTermUrl = (termname.CustomProperties.ContainsKey("Url") ? termname.CustomProperties["Url"] : string.Empty);
            string HtmlBuilder = string.Empty;

            if (termname.Terms.Count > 0)
            {
                htmlBuilder += @"<li><a href='"+ParentTermUrl+"' class='hasdrop''>"+termname.Name+"</a>";
							
                htmlBuilder += "<ul id='drop' class='dropdown'>";
                foreach (Term drpTerm in termname.Terms)
                {
                    ChildTermUrl = (drpTerm.CustomProperties.ContainsKey("Url") ? drpTerm.CustomProperties["Url"] : string.Empty);
                    htmlBuilder += @" <li><a href='" + ChildTermUrl + "'>" + drpTerm.Name + "</a></li>";
                }
                htmlBuilder += "</ul></li>";
            }
            else
            {
                
                htmlBuilder += @" <li><a href='" + ParentTermUrl + "'>" + termname.Name + "</a></li>";
            }

            return htmlBuilder;
        }


        public static SPListItemCollection LookUpQuery(SPWeb web, string lookupname,string listName)
        {
            SPListItemCollection itemCollection = null;

            SPList spList = web.Lists.TryGetList(listName);
            if (spList != null)
            {
                SPQuery qry = new SPQuery();
                qry.Query =
                         @"   <Where>
                          <Eq>
                             <FieldRef Name='FooterTitle' />
                             <Value Type='Lookup'>"+lookupname+@"</Value>
                          </Eq>
                       </Where>";
                itemCollection = spList.GetItems(qry);
            } 

            return itemCollection;

        }

        public static SPListItemCollection FooterHeadings(SPWeb web,string listName)
        {
            SPListItemCollection itemCollection = null;
            SPList spList = web.Lists.TryGetList(listName);
            if (spList != null)
            {
                SPQuery qry = new SPQuery();
                qry.Query =@"   <OrderBy>
                      <FieldRef Name='Title' />
                   </OrderBy>";
                itemCollection = spList.GetItems(qry);
            } 
            return itemCollection;

        }
    }
}
