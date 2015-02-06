using System.Collections.Generic;
using LGH.Model.Entities;

namespace LGH.Model.IRepositories
{
    public interface ICarouselRepository
    {
        ///// <summary>
        ///// Get Carousel Info From SharePoint List
        ///// </summary>
        ///// <returns></returns>


        IList<CarouselHomePage> GetCarouselInfoList();

        void AddCarouselItems(CarouselHomePage CarouselEntity);

        string AddCarouselFileStreamToLibrary(byte[] fileContent, string fileName);

        IList<CarouselHomePage> GetCarouselInfosListById(long itemId);

        IList<CarouselHomePage> GetAllCarouselInfoList();

        string UpdateCarouselItems(CarouselHomePage carouselEntity, long itemId);

      
    }
}
