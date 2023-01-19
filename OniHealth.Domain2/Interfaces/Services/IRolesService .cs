using OniHealth.Domain.Models;
using System.Collections.Generic;

namespace OniHealth.Domain.Interfaces.Services
{
    public interface IRolesService<TEntity> where TEntity : class
    {
        #region Sync
        TEntity Delete(int id);
        TEntity Update(Roles roles);
        #endregion

        #region Async
        Task<TEntity> CreateAsync(Roles roles);
        #endregion
    }
}