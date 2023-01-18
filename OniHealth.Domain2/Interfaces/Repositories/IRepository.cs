using OniHealth.Domain.Models;
using System.Collections.Generic;

namespace OniHealth.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        #region Sync
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entitys);
        TEntity Delete(TEntity entity);
        IEnumerable<TEntity> DeleteRange(IEnumerable<TEntity> entitys);
        #endregion

        #region Async
        Task<int> GetLastId();
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> CreateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entitys);
        #endregion
    }
}