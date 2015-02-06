using System;
using System.Collections.Generic;
using LGH.Model.Entities;

namespace LGH.UI.Forms.Views
{
    public interface ICarouselHomeView
    {
        /// <summary>
        /// Event Handler For Load Data from SharePoint List
        /// </summary>
        event EventHandler LoadData;

        /// <summary>
        /// Set and Get Values in to object from SharePoint List
        /// </summary>
        IList<CarouselHomePage> CarouselInfoList { get; set; }
    }
}