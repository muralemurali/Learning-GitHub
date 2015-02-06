using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LGH.Model.Entities;


namespace LGH.Model.IRepositories
{
    public interface INewsRepository
    {

        IList<LGHNews> GetNewsInfoList();

        void AddNewslItems(LGHNews NewsEntity);

        IList<LGHNews> GetNewsInfosListById(long itemId);

        IList<LGHNews> GetAllNewsInfoList();

        string UpdateNewsItems(LGHNews NewsEntity, long itemId);
    }
}
