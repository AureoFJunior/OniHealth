using OniHealth.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniHealth.Domain.Interfaces.Services
{
    public interface ICustomerService<TEntity> where TEntity : class
    {
        #region Sync
        Customer Delete(int id);
        Customer Update(Customer customer);
        #endregion

        #region Async
        Task<Customer> CreateAsync(Customer customer);
        #endregion
    }
}
