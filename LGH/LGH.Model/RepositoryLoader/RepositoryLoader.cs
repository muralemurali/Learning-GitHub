using System;
using System.Web.Configuration; //IUnityContainer Declaration
using Microsoft.Practices.Unity; //UnityConfigurationSection Declaration
using Microsoft.Practices.Unity.Configuration; //WebConfigurationManager Declaration

namespace LGH.Model.RepositoryLoader
{
    public class RepositoryLoader : IRepositoryLoader
    {
        #region Variable Declaration

        readonly IUnityContainer _container;

        #endregion

        #region Constructor

        /// <summary>
        /// Load the repository in constructor to get the object
        /// </summary>
        public RepositoryLoader()
        {
            _container = new UnityContainer();
            var section = (UnityConfigurationSection)WebConfigurationManager.GetSection("unity");
            section.Configure(_container);
        }

        #endregion

        #region Repository

        /// <summary>
        /// Resolve the container for repository
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetRepository<T>()
        {
            return _container.Resolve<T>();
        }

        #endregion
    }
}
