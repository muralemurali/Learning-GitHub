using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;

namespace LGH.UI.Forms.Classes
{
   public class FormLogics
    {
      
       public static void DeleteListItem(SPWeb web,int ItemIdDelete, string listName)
       {

            SPListItem itemToDelete = null;
            web.AllowUnsafeUpdates = true;
             SPList list = web.Lists.TryGetList(listName);
             if (list != null)
             {
             itemToDelete = list.GetItemById(ItemIdDelete);
             itemToDelete.Delete();
             }

             list.Update();
             web.AllowUnsafeUpdates = false;
                   }
              
         

       

    }
}
