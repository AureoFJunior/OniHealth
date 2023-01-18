using OniHealth.Domain.Models;
using System.Collections.Generic;

namespace OniHealth.Domain.Interfaces.Repositories
{
    public interface IRolesService<TEntity> where TEntity : class
    {
        #region Sync
        Roles Delete(int id);
        Roles Update(Roles roles);
        #endregion

        #region Async
        Task<Roles> CreateAsync(Roles roles);
        #endregion
    }
}