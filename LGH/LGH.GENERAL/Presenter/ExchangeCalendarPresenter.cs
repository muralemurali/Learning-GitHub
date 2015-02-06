using System;
using LGH.Model.Entities;
using LGH.Model.IRepositories;
using LGH.Model.RepositoryLoader;
using LGH.GENERAL.Views;
using Microsoft.SharePoint;
using System.IO;

namespace LGH.GENERAL.Presenter
{
    public class ExchangeCalendarPresenter
    {
        #region Variable Declaration

        protected readonly IExchangeCalendarView CalendarView;
        protected readonly IExchangeCalendarRepository ExchangeRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Set the view in Constructor
        /// </summary>
        /// <param name="view"></param>
        public ExchangeCalendarPresenter(IExchangeCalendarView view)
        {
            CalendarView = view;
            CalendarView.LoadData += _calendarView_LoadData;
            
            IRepositoryLoader loader = new RepositoryLoader();
            ExchangeRepository = loader.GetRepository<IExchangeCalendarRepository>();
        }

        #endregion

        #region Event Handler

        /// <summary>
        /// Call Get Location Details Method to get Location from SharePoint List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _calendarView_LoadData(object sender, EventArgs e)
        {
            GetLocationDetails();
            //GetExchangeCalendarItems();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Get Location From SharePoint List and Bind into Control
        /// </summary>
        private void GetLocationDetails()
        {
            CalendarView.LocationInfoList = ExchangeRepository.GetLocationInfoList();
        }

        public void GetExchangeCalendarItems(string locationName)
        {
            CalendarView.ExchangeInfoList = ExchangeRepository.GetExchangeEventsInfoList(locationName);
        }

        #endregion
    }
}
