using System;
using LGH.Model.Entities;
using LGH.Model.IRepositories;
using LGH.Model.RepositoryLoader;
using LGH.UI.Forms.Views;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Publishing;
using Microsoft.SharePoint.Publishing.Fields;
using System.IO;
using System.Web.UI.WebControls;

namespace LGH.UI.Forms.Presenter
{
    class AddCarouselHomePresenter
    {
        #region Variable Declaration

        protected readonly IAddCarouselHomeView CarouselView;
        protected readonly ICarouselRepository CarouselRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        public AddCarouselHomePresenter(IAddCarouselHomeView view)
        {

            CarouselView = view;
            CarouselView.AddCarouselItem +=CarouselView_AddCarouselItem;

            IRepositoryLoader loader = new RepositoryLoader();
            CarouselRepository = loader.GetRepository<ICarouselRepository>();
        }
/// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
void CarouselView_AddCarouselItem(object sender, EventArgs e)
{
    var thumbnailImageUrl = string.Empty;

    //Upload Image into EventFormImageLibrary
    if (CarouselView.CarouselFileUploadThumbnailImage.HasFile)
    {
        var fileName = Path.GetFileName(CarouselView.CarouselFileUploadThumbnailImage.FileName);

        SPSecurity.RunWithElevatedPrivileges(delegate()
        {
            var fileStream = CarouselView.CarouselFileUploadThumbnailImage.FileBytes;

            var result = AddFileStreamToLibrary(fileStream, fileName);
            if (result.Contains("success"))
            {
                thumbnailImageUrl = "/" + result.Split('|')[1];
            }
        });
    }

    var carouselEntity = new CarouselHomePage
    {
        CarouselImage = thumbnailImageUrl,
        CarouselTitle = CarouselView.CarouselTitle,
        CarouselDescription = CarouselView.CarouselDescription,
        CarouselUrl = CarouselView.CarouselUrl

    };
    AddCarouselItems(carouselEntity);
}

        
      
        #endregion

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EUOfferingsEntity"></param>
private void AddCarouselItems(CarouselHomePage carouselEntity)
        {
            CarouselRepository.AddCarouselItems(carouselEntity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileContent"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string AddFileStreamToLibrary(byte[] fileContent, string fileName)
        {
            return CarouselRepository.AddCarouselFileStreamToLibrary(fileContent, fileName);
        }

        #endregion

    }
}
