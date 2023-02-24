using OniHealth.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniHealth.Domain.Interfaces.Services
{
    public interface IPlanService<TEntity> where TEntity : class
    {
        #region Sync
        TEntity Delete(int id);
        TEntity Update(Plans plan);
        #endregion

        #region Async
        Task<TEntity> CreateAsync(Plans plan);
        #endregion
    }
}
