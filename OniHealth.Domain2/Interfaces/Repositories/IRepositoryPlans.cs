using OniHealth.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniHealth.Domain.Interfaces.Repositories
{
    public interface IRepositoryPlans : IRepository<Plans>
    {
        #region Async
        Task<string> GetNameByIdAsync(int id);
        Task<string> GetNameByCustomerAsync(int customerId);
        #endregion
    }
}
