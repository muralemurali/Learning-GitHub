using System;

namespace LGH.Model.RepositoryLoader
{
    public interface IRepositoryLoader
    {
        /// <summary>
        /// Get object for current view
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetRepository<T>();
    }
}
