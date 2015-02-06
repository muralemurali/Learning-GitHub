using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;

namespace LGH.GENERAL.Classes
{
    public class MeetingRoomInfo
    {
        public static SPListItemCollection ListItemCollection(SPWeb web,string listName)
        {
            SPListItemCollection listItems=null;
            SPList spList = web.Lists.TryGetList(listName);
            if (spList != null)
            {
                SPQuery qry = new SPQuery();
                qry.ViewFields = @"<FieldRef Name='MeetingRoom' />";
                 listItems = spList.GetItems(qry);
            }

            return listItems;

        }
    }
}
