using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LGH.Model.Entities;

namespace LGH.UI.Forms.Views.REPORTSLINKS
{
    public interface IReportsHomeView
    {
        /// <summary>
        /// Event Handler For Load Data from SharePoint List
        /// </summary>
        event EventHandler LoadData;

        /// <summary>
        /// Set and Get Values in to object from SharePoint List
        /// </summary>
        IList<LGHReports> ReportsInfoList { get; set; }


    }
}
