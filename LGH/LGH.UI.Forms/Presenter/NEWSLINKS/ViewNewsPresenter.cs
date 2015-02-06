using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LGH.UI.Forms.Views.NEWSLINKS;
using LGH.Model.Entities;
using LGH.Model.IRepositories;
using LGH.Model.RepositoryLoader;

namespace LGH.UI.Forms.Presenter.NEWSLINKS
{
    public class ViewNewsPresenter
    {

        #region Variable Declaration

        protected readonly INewsHomeView _newsView;
        protected readonly INewsRepository _newsRepository;


        #endregion

        #region Constructor
        public ViewNewsPresenter(INewsHomeView view)
        {
            _newsView = view;
            _newsView.LoadData += _carouselView_LoadData;
            IRepositoryLoader loader = new RepositoryLoader();
            _newsRepository = loader.GetRepository<INewsRepository>();
        }

        void _carouselView_LoadData(object sender, EventArgs e)
        {
            GetNewsInfo();
        }




        #endregion

        #region Private Methods

        private void GetNewsInfo()
       {
           _newsView.NewsInfoList = _newsRepository.GetAllNewsInfoList();
       }
        #endregion
    }
}
