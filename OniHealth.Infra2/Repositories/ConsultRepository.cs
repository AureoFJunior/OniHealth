using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OniHealth.Domain.Models;
using OniHealth.Infra.Context;

namespace OniHealth.Infra.Repositories
{
    public class ConsultRepository : Repository<Employer>
    {
        private readonly UnitOfWork unitOfWork;
        public ConsultRepository(AppDbContext context) : base(context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public async override Task<Employer> GetByIdAsync(int id)
        {
            var query = _context.Set<Employer>().Where(e => e.Id == id).AsNoTracking();

            if (await query.AnyAsync())
                return await query.FirstOrDefaultAsync();

            return null;
        }

        public async override Task<IEnumerable<Employer>> GetAllAsync()
        {
            var query = _context.Set<Employer>();

            return await query.AnyAsync() ? await query.AsNoTracking().ToListAsync() : new List<Employer>();
        }
    }
}