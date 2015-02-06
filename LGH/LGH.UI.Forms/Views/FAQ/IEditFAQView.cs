using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LGH.Model.Entities;
using System.Web.UI.WebControls;

namespace LGH.UI.Forms.Views
{
   public interface IEditFAQView
    {

        event EventHandler LoadData;

        /// <summary>
        /// Event Handler For Updating Data Into SharePoint List
        /// </summary>
        event EventHandler UpdateFAQItem;

        /// <summary>
        /// Set and Get Values in to object from SharePoint List
        /// </summary>
        IList<LGHFAQItems> FAQListInfo { get; set; }


        string QueryStringItemId { get; }

        /// <summary>
        /// Get answer from faq
        /// </summary>
        string FaqAnswer { get; }

        /// <summary>
        /// Get Question from FAq
        /// </summary>
        string FaqQuest { get; }

    }
}
