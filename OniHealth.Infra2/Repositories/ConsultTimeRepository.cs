using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OniHealth.Domain.Interfaces.Repositories;
using OniHealth.Domain.Models;
using OniHealth.Infra.Context;

namespace OniHealth.Infra.Repositories
{
    public class ConsultTimeRepository : Repository<ConsultTime>
    {
        public ConsultTimeRepository(AppDbContext context) : base(context)
        {
        }

        public async override Task<ConsultTime> GetByIdAsync(int id)
        {
            var query = _context.Set<ConsultTime>().Where(e => e.Id == id).AsNoTracking();

            if (await query.AnyAsync())
                return await query.FirstOrDefaultAsync();

            return null;
        }

        public async override Task<IEnumerable<ConsultTime>> GetAllAsync()
        {
            var query = _context.Set<ConsultTime>();

            return await query.AnyAsync() ? await query.AsNoTracking().ToListAsync() : new List<ConsultTime>();
        }
    }
}