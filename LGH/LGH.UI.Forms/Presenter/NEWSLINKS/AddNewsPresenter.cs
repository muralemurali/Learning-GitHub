using System;
using LGH.Model.Entities;
using LGH.Model.IRepositories;
using LGH.Model.RepositoryLoader;
using LGH.UI.Forms.Views.NEWSLINKS;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Publishing;
using Microsoft.SharePoint.Publishing.Fields;
using System.IO;
using System.Web.UI.WebControls;

namespace LGH.UI.Forms.Presenter.NEWSLINKS
{
    public class AddNewsPresenter
    {
        #region Variable Declaration

        protected readonly  IAddNewsView newsView;
        protected readonly INewsRepository newsRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        public AddNewsPresenter(IAddNewsView view)
        {

            newsView = view;
            newsView.AddNewsItem += newsView_AddNewsItem;

            IRepositoryLoader loader = new RepositoryLoader();
            newsRepository = loader.GetRepository<INewsRepository>();
        }

        void newsView_AddNewsItem(object sender, EventArgs e)
        {
            var newsEntity = new LGHNews
            {

                Url = newsView.LinkUrl,
                shortNotes = newsView.shortNotes

            };
            AddNewsItems(newsEntity);
            
        }

        


        
      
        #endregion

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EUOfferingsEntity"></param>
private void AddNewsItems(LGHNews newsEntity)
        {
            newsRepository.AddNewslItems(newsEntity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileContent"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        
        #endregion
    }
}
