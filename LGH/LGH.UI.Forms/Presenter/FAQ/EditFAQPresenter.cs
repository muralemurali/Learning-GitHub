using System;
using LGH.Model.Entities;
using LGH.Model.IRepositories;
using LGH.Model.RepositoryLoader;
using LGH.UI.Forms.Views;
using Microsoft.SharePoint;
using System.IO;


namespace LGH.UI.Forms.Presenter
{
   public class EditFAQPresenter
    {



           #region Variable Declaration

        protected readonly IEditFAQView EditFAQView;
        protected readonly IFaqRepository EditFAQRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Set the view in Constructor
        /// </summary>
        /// <param name="view"></param>
        public EditFAQPresenter(IEditFAQView view)
        {
            EditFAQView = view;
            EditFAQView.LoadData +=EditFAQView_LoadData;
            EditFAQView.UpdateFAQItem += EditFAQView_UpdateFAQItem;

            IRepositoryLoader loader = new RepositoryLoader();
            EditFAQRepository = loader.GetRepository<IFaqRepository>();
        }

        void EditFAQView_UpdateFAQItem(object sender, EventArgs e)
        {

             var FaqEntity = new LGHFAQItems
            {
                Answer = EditFAQView.FaqAnswer,
                Question = EditFAQView.FaqQuest
              
                
            };

             UpdateFAQItem(FaqEntity, Convert.ToInt64(EditFAQView.QueryStringItemId));
        
           
        }

       

        #endregion

        #region Event Handler

        /// <summary>
        /// Call Get FAq Info method to get data from SP List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        void EditFAQView_LoadData(object sender, EventArgs e)
        {
            GetFAQInfo(Convert.ToInt64(EditFAQView.QueryStringItemId));

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Get All FAQlist Information and Bind into the controls
        /// </summary>
        private void GetFAQInfo(long itemId)
        {
            EditFAQView.FAQListInfo = EditFAQRepository.GetFAQInfoListById(itemId);
        }

        /// <summary>
        /// Update Items into FAQ List
        /// </summary>
        private void UpdateFAQItem(LGHFAQItems FAQEntity, long itemId)
        {
            EditFAQRepository.UpdateFAQItems(FAQEntity, itemId);
        }

        #endregion
    }
}
