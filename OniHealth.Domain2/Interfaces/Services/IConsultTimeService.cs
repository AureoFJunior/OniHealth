using OniHealth.Domain.Models;
using System.Collections.Generic;

namespace OniHealth.Domain.Interfaces.Services
{
    public interface IConsultTimeService<TEntity> where TEntity : class
    {
        #region Sync
        #endregion

        #region Async
        Task<ConsultTime> CreateAsync(string queueName);
        #endregion
    }
}