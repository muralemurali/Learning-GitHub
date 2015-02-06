using System;
using LGH.Model.Entities;
using LGH.Model.IRepositories;
using LGH.Model.RepositoryLoader;
using LGH.UI.Forms.Views.REPORTSLINKS;

namespace LGH.UI.Forms.Presenter
{
   public class ViewReportPresenter
    {

        #region Variable Declaration

        protected readonly IReportsHomeView _reportsView;
        protected readonly IReportsRepository _reportsRepository;


        #endregion

        #region Constructor
        public ViewReportPresenter(IReportsHomeView view)
        {
            _reportsView = view;
            _reportsView.LoadData += _carouselView_LoadData;
            IRepositoryLoader loader = new RepositoryLoader();
            _reportsRepository = loader.GetRepository<IReportsRepository>();
        }

        void _carouselView_LoadData(object sender, EventArgs e)
        {
            GetReportslInfo();
        }




        #endregion

        #region Private Methods

        private void GetReportslInfo()
       {
           _reportsView.ReportsInfoList = _reportsRepository.GetAllReportsInfoList();
       }
        #endregion
    }
}
