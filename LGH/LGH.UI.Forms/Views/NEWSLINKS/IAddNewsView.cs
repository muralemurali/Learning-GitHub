using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGH.UI.Forms.Views.NEWSLINKS
{
    public interface IAddNewsView
    {
        event EventHandler AddNewsItem;

        string shortNotes { get; }

        string LinkUrl { get; }

    }
}
