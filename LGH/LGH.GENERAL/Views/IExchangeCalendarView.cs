using System;
using System.Collections.Generic;
using LGH.Model.Entities;
using System.Web.UI.WebControls;
using System.Data;


namespace LGH.GENERAL.Views
{
    public interface IExchangeCalendarView
    {
        /// <summary>
        /// Event Handler For Load Data from SharePoint List
        /// </summary>
        event EventHandler LoadData;

        /// <summary>
        /// Set and Get Values in to object from SharePoint List
        /// </summary>
        IList<LGHExchangeItems> ExchangeInfoList { get; set; }

        /// <summary>
        /// Get the value of Location from SharePoint List and bind into Dropdown List
        /// </summary>
        DropDownList LocationDropDownList { get; }

        ///// <summary>
        ///// Set and Get Values in to object from SharePoint List
        ///// </summary>
        //DataTable LocationTable { get; set; }

        /// <summary>
        /// Set and Get Values in to object from SharePoint List
        /// </summary>
        IList<LGHLocation> LocationInfoList { get; set; }
    }
}
