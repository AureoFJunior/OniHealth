using Microsoft.EntityFrameworkCore;
using OniHealth.Domain.Interfaces;
using OniHealth.Domain.Models;
using OniHealth.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniHealth.Infra.Repositories
{
    public class CustomerRepository : Repository<Customer>
    {
        private readonly UnitOfWork _unitOfWork;

        public CustomerRepository(AppDbContext context) : base(context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            var query = _context.Set<Customer>().Where(c => c.Id == id).AsNoTracking();

            if (await query.AnyAsync())
            {
                return await query.FirstOrDefaultAsync();
            }

            return null;
        }

        public async override Task<IEnumerable<Customer>> GetAllAsync()
        {
            var query = _context.Set<Customer>();

            return await query.AnyAsync() ? await query.AsNoTracking().ToListAsync() : new List<Customer>();
        }
    }
}
