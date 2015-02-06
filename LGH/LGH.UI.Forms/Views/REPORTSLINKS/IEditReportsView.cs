﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LGH.Model.Entities;

namespace LGH.UI.Forms.Views.REPORTSLINKS
{
    public interface IEditReportsView
    {
        /// <summary>
        /// Event Handler For Load Data from SharePoint List
        /// </summary>
        event EventHandler LoadData;

        /// <summary>
        /// Event Handler For Updating Data Into SharePoint List
        /// </summary>
        event EventHandler UpdateReportsItem;

        /// <summary>
        /// Set and Get Values in to object from SharePoint List
        /// </summary>
        IList<LGHReports> ReportsInfoList { get; set; }

        /// <summary>
        /// Query String ItemId
        /// </summary>
        string QueryStringItemId { get; }

        /// <summary>
        /// Get value from carouseltitle
        /// </summary>
        string LinkUrl { get; }

        /// <summary>
        /// Get value from ImageDescription
        /// </summary>
        string shortNotes { get; }


    }
}
