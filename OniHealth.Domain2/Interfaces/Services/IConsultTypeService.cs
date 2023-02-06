using OniHealth.Domain.Models;
using System.Collections.Generic;

namespace OniHealth.Domain.Interfaces.Services
{
    public interface IConsultTypeService<TEntity> where TEntity : class
    {
        #region Sync
        #endregion

        #region Async
        Task<ConsultType> CreateAsync(string queueName);
        #endregion
    }
}