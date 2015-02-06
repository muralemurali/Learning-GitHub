using System;
using LGH.Model.Entities;
using LGH.Model.IRepositories;
using LGH.Model.RepositoryLoader;
using LGH.UI.Forms.Views;
using Microsoft.SharePoint;
using System.IO;


namespace LGH.UI.Forms.Presenter
{
   public class EditCarouselPresenter
    {
        #region Variable Declaration

        protected readonly IEditCarouselView EditCarouselView;
        protected readonly ICarouselRepository EditCarouselRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Set the view in Constructor
        /// </summary>
        /// <param name="view"></param>
        public EditCarouselPresenter(IEditCarouselView view)
        {
            EditCarouselView = view;
            EditCarouselView.LoadData += EditEuOfferView_LoadData;
            EditCarouselView.UpdateCarouselItem += EditCarouselView_UpdateCarouselItem;

            IRepositoryLoader loader = new RepositoryLoader();
            EditCarouselRepository = loader.GetRepository<ICarouselRepository>();
        }

       
       

        #endregion

        #region Event Handler

        /// <summary>
        /// Call Get Sponsorship Info method to get data from SP List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void EditEuOfferView_LoadData(object sender, EventArgs e)
        {
            GetCarouselInfo(Convert.ToInt64(EditCarouselView.QueryStringItemId));
        }

        void EditCarouselView_UpdateCarouselItem(object sender, EventArgs e)
        {
            var thumbnailImageUrl = string.Empty;

            if (EditCarouselView.ThumbnailImage.HasFile)
            {
                var fileName = Path.GetFileName(EditCarouselView.ThumbnailImage.FileName);

                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    var fileStream = EditCarouselView.ThumbnailImage.FileBytes;

                    var result = AddFileStreamToLibrary(fileStream, fileName);
                    if (result.Contains("success"))
                    {
                        thumbnailImageUrl = "/" + result.Split('|')[1];
                    }
                });
            }
            else
            {
                thumbnailImageUrl = EditCarouselView.FileUploadImageUrl;
            }
            var carouselEntity = new CarouselHomePage
            {
                CarouselTitle = EditCarouselView.carouselTitle,
                CarouselDescription = EditCarouselView.carouselDescription,
                CarouselImage = thumbnailImageUrl,
                CarouselUrl=EditCarouselView.carouselUrl
                
            };

            updateCarouselItem(carouselEntity, Convert.ToInt64(EditCarouselView.QueryStringItemId));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Get All Sponsorship Information and Bind into the controls
        /// </summary>
        private void GetCarouselInfo(long itemId)
        {
            EditCarouselView.carouselInfoList = EditCarouselRepository.GetCarouselInfosListById(itemId);
        }

        /// <summary>
        /// Update Items into Sponsorship List
        /// </summary>
        private void updateCarouselItem(CarouselHomePage carouselEntity, long itemId)
        {
            EditCarouselRepository.UpdateCarouselItems(carouselEntity, itemId);
        }

        /// <summary>
        /// Upload Image File into Library
        /// </summary>
        /// <param name="fileContent"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string AddFileStreamToLibrary(byte[] fileContent, string fileName)
        {
            return EditCarouselRepository.AddCarouselFileStreamToLibrary(fileContent, fileName);
        }

        #endregion



    }
}
