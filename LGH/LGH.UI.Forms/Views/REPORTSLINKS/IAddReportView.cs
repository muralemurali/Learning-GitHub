using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGH.UI.Forms.Views.REPORTSLINKS
{
    public interface IAddReportView
    {

        event EventHandler AddReportsItem;

        //string CarouselTitle { get; }


        string shortNotes { get; }

        string LinkUrl { get; }
    }
}
