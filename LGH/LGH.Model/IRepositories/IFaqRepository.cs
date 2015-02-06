using System.Collections.Generic;
using LGH.Model.Entities;

namespace LGH.Model.IRepositories
{
    public interface IFaqRepository
    {
        /// <summary>
        /// Get Faq Info From SharePoint List
        /// </summary>
        /// <returns></returns>
        IList<LGHFAQItems> GetFaqInfoList();

        /// <summary>
        /// Get Faq Info From SharePoint List
        /// </summary>
        /// <returns></returns>
        IList<LGHFAQItems> GetAllFaqInfoList();

        /// <summary>
        /// Add Faq Items into SharePoint List
        /// </summary>
        /// <param name="ssqFaqEntity"></param>
        void AddFaqItems(LGHFAQItems ssqFaqEntity);

        IList<LGHFAQItems> GetFAQInfoListById(long itemId);

        string UpdateFAQItems(LGHFAQItems FAQEntity, long itemId);

       // void DeleteFAQItems(int ItemID);
    }
}
