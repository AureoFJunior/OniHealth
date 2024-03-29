using OniHealth.Domain.Models;
using System.Collections.Generic;

namespace OniHealth.Domain.Interfaces.Services
{
    public interface IEmployerService<TEntity> where TEntity : class
    {
        #region Sync
        TEntity Delete(int id);
        TEntity Update(Employer employer);
        #endregion

        #region Async
        Task<TEntity> CreateAsync(Employer employer);
        #endregion
    }
}