using System;
using LGH.Model.Entities;
using LGH.Model.IRepositories;
using LGH.Model.RepositoryLoader;
using LGH.UI.Forms.Views.REPORTSLINKS;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Publishing;
using Microsoft.SharePoint.Publishing.Fields;
using System.IO;
using System.Web.UI.WebControls;

namespace LGH.UI.Forms.Presenter
{
    public class AddReportPresenter
    {
        #region Variable Declaration

        protected readonly IAddReportView reportsView;
        protected readonly IReportsRepository reportsRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        public AddReportPresenter(IAddReportView view)
        {

            reportsView = view;
            reportsView.AddReportsItem += reportsView_AddReportsItem;

            IRepositoryLoader loader = new RepositoryLoader();
            reportsRepository = loader.GetRepository<IReportsRepository>();
        }

        void reportsView_AddReportsItem(object sender, EventArgs e)
        {
          

            //Upload Image into EventFormImageLibrary
            

            var carouselEntity = new LGHReports
            {
                
                Url = reportsView.LinkUrl,
                shortNotes=reportsView.shortNotes

            };
            AddReportsItems(carouselEntity);
        }
/// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        
      
        #endregion

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EUOfferingsEntity"></param>
private void AddReportsItems(LGHReports reportEntity)
        {
            reportsRepository.AddReportsItems(reportEntity);
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
