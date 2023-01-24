using OniHealth.Domain.Models;
using System.Collections.Generic;

namespace OniHealth.Domain.Interfaces.Services
{
    public interface IConsultService<TEntity> where TEntity : class
    {
        #region Sync
        TEntity Delete(int id);
        TEntity Update(Consult consult);
        #endregion

        #region Async
        Task<TEntity> CreateAsync(Consult consult);
        #endregion
    }
}