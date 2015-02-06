using System.Data;
using LGH.Model.Entities;
using System.Collections.Generic;

namespace LGH.Model.IRepositories
{
    public interface IExchangeCalendarRepository
    {
        /// <summary>
        /// Get Location Details
        /// </summary>
        /// <returns></returns>
        IList<LGHLocation> GetLocationInfoList();

        /// <summary>
        /// Get Exchange Event Type Items from List in SharePoint Site
        /// </summary>
        /// <param name="locationName"></param>
        /// <returns></returns>
        IList<LGHExchangeItems> GetExchangeEventsInfoList(string locationName);
    }
}
