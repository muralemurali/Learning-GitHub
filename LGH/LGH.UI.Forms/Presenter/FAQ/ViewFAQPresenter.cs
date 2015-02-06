using System;
using LGH.Model.Entities;
using LGH.Model.IRepositories;
using LGH.Model.RepositoryLoader;
using LGH.UI.Forms.Views;

namespace LGH.UI.Forms.Presenter
{
    public class ViewFAQPresenter
    {
        
       #region Variable Declaration

       protected readonly IFaqView FAQView;
       protected readonly IFaqRepository FAQRepository;


       #endregion

       #region Constructor
       public ViewFAQPresenter(IFaqView view)
       {
           FAQView = view;
           FAQView.LoadData += FAQView_LoadData;
           IRepositoryLoader loader = new RepositoryLoader();
           FAQRepository = loader.GetRepository<IFaqRepository>();
       }

       void FAQView_LoadData(object sender, EventArgs e)
       {
           GetFAQinfo();
       }



       #endregion


       #region Private Methods

       public void GetFAQinfo()
       {
           FAQView.FAQInfoList = FAQRepository.GetAllFaqInfoList();
       }

       #endregion


    }
}
