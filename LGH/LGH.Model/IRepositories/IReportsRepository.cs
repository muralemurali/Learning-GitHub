using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LGH.Model.Entities;

namespace LGH.Model.IRepositories
{
    public interface IReportsRepository
    {

        IList<LGHReports> GetReportsInfoList();

        void AddReportsItems(LGHReports ReportsEntity);

        IList<LGHReports> GetReportsInfosListById(long itemId);

        IList<LGHReports> GetAllReportsInfoList();

        string UpdateReportsItems(LGHReports ReportsEntity, long itemId);

    }
}
