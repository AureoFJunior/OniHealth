using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OniHealth.Domain.Interfaces.Repositories;
using OniHealth.Domain.Models;
using OniHealth.Infra.Context;

namespace OniHealth.Infra.Repositories
{
    public class ConsultRepository : Repository<Consult>, IRepositoryConsult
    {
        private readonly UnitOfWork unitOfWork;
        public ConsultRepository(AppDbContext context) : base(context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public async override Task<Consult> GetByIdAsync(int id)
        {
            var query = _context.Set<Consult>().Where(e => e.Id == id).AsNoTracking();

            if (await query.AnyAsync())
                return await query.FirstOrDefaultAsync();

            return null;
        }

        public async override Task<IEnumerable<Consult>> GetAllAsync()
        {
            var query = _context.Set<Consult>();

            return await query.AnyAsync() ? await query.AsNoTracking().ToListAsync() : new List<Consult>();
        }
    }
}