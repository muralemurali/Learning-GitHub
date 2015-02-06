using System;
using LGH.Model.Entities;
using LGH.Model.IRepositories;
using LGH.Model.RepositoryLoader;
using LGH.UI.Forms.Presenter;
using LGH.UI.Forms.Views;


namespace LGH.UI.Forms.Presenter
{
    public class AddFaqPresenter
    {
        #region Variable Declaration

        protected readonly IAddFaqView FaqView;
        protected readonly IFaqRepository FaqRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Set the view in Constructor
        /// </summary>
        /// <param name="view"></param>
        public AddFaqPresenter(IAddFaqView view)
        {
            FaqView = view;
            FaqView.SaveFaqItems += FaqView_SaveFaqItems;

            IRepositoryLoader loader = new RepositoryLoader();
            FaqRepository = loader.GetRepository<IFaqRepository>();
        }

        #endregion

        #region Event Handler

        /// <summary>
        /// Call Get Faq Info method to get data from SP List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FaqView_SaveFaqItems(object sender, EventArgs e)
        {
            var faqpEntity = new LGHFAQItems
            {
                Question = FaqView.Question,
                Answer = FaqView.Answer
            };

            SaveFaqInfo(faqpEntity);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Get Faq List Information and Bind into the controls
        /// </summary>
        private void SaveFaqInfo(LGHFAQItems faqEntity)
        {
            FaqRepository.AddFaqItems(faqEntity);
        }

        #endregion
    }
}
