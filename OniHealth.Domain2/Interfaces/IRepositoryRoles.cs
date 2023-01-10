using OniHealth.Domain.Models;
using System.Collections.Generic;

namespace OniHealth.Domain.Interfaces
{
    public interface IRepositoryRoles : IRepository<Roles>
    {
        
        #region Sync

        #endregion

        #region Async
        Task<string> GetNameByIdAsync(int id);
        Task<string> GetNameByEmployerAsync(int employerId);
        #endregion
    }
}