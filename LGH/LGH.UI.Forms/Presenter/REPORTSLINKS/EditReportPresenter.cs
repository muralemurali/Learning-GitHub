using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LGH.Model.Entities;
using LGH.Model.IRepositories;
using LGH.Model.RepositoryLoader;
using LGH.UI.Forms.Views.REPORTSLINKS;

namespace LGH.UI.Forms.Presenter.REPORTSLINKS
{
   public  class EditReportPresenter
    {
        #region Variable Declaration

        protected readonly IEditReportsView EditReportsView;
        protected readonly IReportsRepository EditReportsRepository;

        #endregion


        #region Constructor

        /// <summary>
        /// Set the view in Constructor
        /// </summary>
        /// <param name="view"></param>
        public EditReportPresenter(IEditReportsView view)
        {
            EditReportsView = view;
            EditReportsView.LoadData += EditReportsView_LoadData;
            EditReportsView.UpdateReportsItem += EditReportsView_UpdateReportsItem;
            IRepositoryLoader loader = new RepositoryLoader();
            EditReportsRepository = loader.GetRepository<IReportsRepository>();
        }

        void EditReportsView_LoadData(object sender, EventArgs e)
        {
            GetReportsInfo(Convert.ToInt64(EditReportsView.QueryStringItemId));
        }

       
       
       

        #endregion

        #region Event Handler

        /// <summary>
        /// Call Get Sponsorship Info method to get data from SP List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        void EditReportsView_UpdateReportsItem(object sender, EventArgs e)
        {
           
            var thumbnailImageUrl = string.Empty;

           
            var reportsEntity = new LGHReports
            {
               shortNotes=EditReportsView.shortNotes,
               Url=EditReportsView.LinkUrl
            };

            updateReportsItem(reportsEntity, Convert.ToInt64(EditReportsView.QueryStringItemId));
        
        }

       

        #endregion

        #region Private Methods

        /// <summary>
        /// Get All Sponsorship Information and Bind into the controls
        /// </summary>
        private void GetReportsInfo(long itemId)
        {
            EditReportsView.ReportsInfoList = EditReportsRepository.GetReportsInfosListById(itemId);
        }

        /// <summary>
        /// Update Items into Sponsorship List
        /// </summary>
        private void updateReportsItem(LGHReports reportsEntity, long itemId)
        {
            EditReportsRepository.UpdateReportsItems(reportsEntity, itemId);
        }

        
       

        #endregion

    }
}
