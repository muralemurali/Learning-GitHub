using System;
using LGH.Model.Entities;
using LGH.Model.IRepositories;
using LGH.Model.RepositoryLoader;
using LGH.UI.Forms.Views;

namespace LGH.UI.Forms.Presenter
{
    class ViewCarouselPresenter
    {

         #region Variable Declaration

       protected readonly ICarouselHomeView _carouselView;
       protected readonly ICarouselRepository _carouselRepository;


       #endregion

       #region Constructor
       public ViewCarouselPresenter(ICarouselHomeView view)
       {
           _carouselView = view;
           _carouselView.LoadData += _carouselView_LoadData;
           IRepositoryLoader loader = new RepositoryLoader();
           _carouselRepository = loader.GetRepository<ICarouselRepository>();
       }

       void _carouselView_LoadData(object sender, EventArgs e)
       {
           GetCarouselInfo();
       }

       
       
       
       #endregion

       #region Private Methods

       private void GetCarouselInfo()
       {
           _carouselView.CarouselInfoList = _carouselRepository.GetAllCarouselInfoList();
       }
       #endregion
    }
}
