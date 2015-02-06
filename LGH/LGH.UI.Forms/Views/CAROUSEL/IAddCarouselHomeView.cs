using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace LGH.UI.Forms.Views
{
    public interface IAddCarouselHomeView
    {

        event EventHandler AddCarouselItem;

        string CarouselTitle { get; }


        string CarouselDescription { get; }

        string CarouselUrl { get; }

        FileUpload CarouselFileUploadThumbnailImage { get; }

    }
}
