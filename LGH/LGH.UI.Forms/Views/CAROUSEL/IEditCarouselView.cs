using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LGH.Model.Entities;
using System.Web.UI.WebControls;

namespace LGH.UI.Forms.Views
{
   public interface IEditCarouselView
    {
        /// <summary>
        /// Event Handler For Load Data from SharePoint List
        /// </summary>
        event EventHandler LoadData;

        /// <summary>
        /// Event Handler For Updating Data Into SharePoint List
        /// </summary>
        event EventHandler UpdateCarouselItem;

        /// <summary>
        /// Set and Get Values in to object from SharePoint List
        /// </summary>
        IList<CarouselHomePage> carouselInfoList { get; set; }

        /// <summary>
        /// Query String ItemId
        /// </summary>
        string QueryStringItemId { get; }

        /// <summary>
        /// Get value from carouseltitle
        /// </summary>
        string carouselTitle { get; }

        /// <summary>
        /// Get value from ImageDescription
        /// </summary>
        string carouselDescription { get; }

        /// <summary>
        /// Get value from ThumnailImage
        /// </summary>
        FileUpload ThumbnailImage { get; }

        string carouselUrl { get; }

        /// <summary>
        /// File Upload ImageUrl
        /// </summary>
        string FileUploadImageUrl { get; }


    }
}
