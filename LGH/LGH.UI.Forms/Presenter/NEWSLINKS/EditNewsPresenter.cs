using System;
using LGH.Model.Entities;
using LGH.Model.IRepositories;
using LGH.Model.RepositoryLoader;
using LGH.UI.Forms.Views.NEWSLINKS;
using Microsoft.SharePoint;
using System.IO;


namespace LGH.UI.Forms.Presenter.NEWSLINKS
{
   public class EditNewsPresenter
    {

        #region Variable Declaration

        protected readonly IEditNewsView EditNewsView;
        protected readonly INewsRepository EditNewsRepository;

        #endregion


        #region Constructor

        /// <summary>
        /// Set the view in Constructor
        /// </summary
        /// <param name="view"></param>
        public EditNewsPresenter(IEditNewsView view)
        {
            EditNewsView = view;
            EditNewsView.LoadData += EditNewsView_LoadData;
            EditNewsView.UpdateNewsItem += EditReportsView_UpdateNewsItem;
            IRepositoryLoader loader = new RepositoryLoader();
            EditNewsRepository = loader.GetRepository<INewsRepository>();
        }

        void EditNewsView_LoadData(object sender, EventArgs e)
        {
            GetNewsInfo(Convert.ToInt64(EditNewsView.QueryStringItemId));
        }

       
       
       

        #endregion

        #region Event Handler

        /// <summary>
        /// Call Get Sponsorship Info method to get data from SP List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        void EditReportsView_UpdateNewsItem(object sender, EventArgs e)
        {
           
            var thumbnailImageUrl = string.Empty;

           
            var newsentity = new LGHNews
            {
               shortNotes=EditNewsView.shortNotes,
               Url=EditNewsView.LinkUrl
            };

            updateReportsItem(newsentity, Convert.ToInt64(EditNewsView.QueryStringItemId));
        
        }

       

        #endregion

        #region Private Methods

        /// <summary>
        /// Get All Sponsorship Information and Bind into the controls
        /// </summary>
        private void GetNewsInfo(long itemId)
        {
            EditNewsView.NewsInfoList = EditNewsRepository.GetNewsInfosListById(itemId);
        }

        /// <summary>
        /// Update Items into Sponsorship List
        /// </summary>
        private void updateReportsItem(LGHNews newsEntity, long itemId)
        {
            EditNewsRepository.UpdateNewsItems(newsEntity, itemId);
        }

        
       

        #endregion

    }
}
