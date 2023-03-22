using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OniHealth.Domain.Interfaces.Repositories;
using OniHealth.Domain.Models;
using OniHealth.Infra.Context;

namespace OniHealth.Infra.Repositories
{
    public class ConsultTypeRepository : Repository<ConsultType>
    {
        public ConsultTypeRepository(AppDbContext context) : base(context)
        {
        }

        public async override Task<ConsultType> GetByIdAsync(int id)
        {
            var query = _context.Set<ConsultType>().Where(e => e.Id == id).AsNoTracking();

            if (await query.AnyAsync())
                return await query.FirstOrDefaultAsync();

            return null;
        }

        public async override Task<IEnumerable<ConsultType>> GetAllAsync()
        {
            var query = _context.Set<ConsultType>();

            return await query.AnyAsync() ? await query.AsNoTracking().ToListAsync() : new List<ConsultType>();
        }
    }
}