using Microsoft.EntityFrameworkCore;
using OniHealth.Domain.Interfaces.Repositories;
using OniHealth.Domain.Interfaces.Services;
using OniHealth.Domain.Models;
using OniHealth.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniHealth.Infra.Repositories
{
    public class PlansRepository : Repository<Plans>, IRepositoryPlans
    {
        private readonly UnitOfWork unitOfWork;
        public PlansRepository(AppDbContext context) : base(context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public async override Task<Plans> GetByIdAsync(int id)
        {
            var query = _context.Set<Plans>().Where(c => c.Id == id).AsNoTracking();

            if(await query.AnyAsync())
                return await query.FirstOrDefaultAsync();

            return null;
        }

        public async override Task<IEnumerable<Plans>> GetAllAsync()
        {
            var query = _context.Set<Plans>();

            return await query.AnyAsync() ? await query.AsNoTracking().ToListAsync() : new List<Plans>();
        }

        public async Task<string> GetNameByCustomerAsync(int customerId)
        {
            var query = _context.Set<Customer>()
                .Where(c => c.SignedPlanId == customerId)
                .Select(c => c.Name)
                .AsNoTracking();

            if (await query.AnyAsync())
                return await query.FirstOrDefaultAsync();

            return null;
        }

        public async Task<string> GetNameByIdAsync(int id)
        {
            var query = _context.Set<Plans>().Where(c => c.Id == id).Select(x => x.Name).AsNoTracking();

            if (await query.AnyAsync())
                return await query.FirstOrDefaultAsync();

            return null;
        }
    }
}
