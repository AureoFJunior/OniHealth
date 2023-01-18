using OniHealth.Domain.Models;
using System.Collections.Generic;

namespace OniHealth.Domain.Interfaces.Repositories
{
    public interface IEmployerService<TEntity> where TEntity : class
    {
        #region Sync
        Employer Delete(int id);
        Employer Update(Employer employer);
        #endregion

        #region Async
        Task<Employer> CreateAsync(Employer employer);
        #endregion
    }
}