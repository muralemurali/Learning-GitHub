using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LGH.Model.Entities;


namespace LGH.UI.Forms.Views
{
    public interface IAddFaqView
    {
        /// <summary>
        /// Event Handler to Add Item into SharePoint List
        /// </summary>
        event EventHandler SaveFaqItems;

        /// <summary>
        /// Question
        /// </summary>
        string Question { get; }

        /// <summary>
        /// Answer
        /// </summary>
        string Answer { get; }
    }
}
