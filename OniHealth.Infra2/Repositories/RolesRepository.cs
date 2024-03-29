using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OniHealth.Domain.Interfaces.Repositories;
using OniHealth.Domain.Models;
using OniHealth.Infra.Context;

namespace OniHealth.Infra.Repositories
{
    public class RolesRepository : Repository<Roles>, IRepositoryRoles
    {
        private readonly UnitOfWork unitOfWork;
        public RolesRepository(AppDbContext context) : base(context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public async override Task<Roles> GetByIdAsync(int id)
        {
            var query = _context.Set<Roles>().Where(e => e.Id == id).AsNoTracking();

            if (await query.AnyAsync())
                return await query.FirstOrDefaultAsync();

            return null;
        }

        public async override Task<IEnumerable<Roles>> GetAllAsync()
        {
            var query = _context.Set<Roles>();

            return await query.AnyAsync() ? await query.AsNoTracking().ToListAsync() : new List<Roles>();
        }

        public async Task<string> GetNameByIdAsync(int id)
        {
            var query = _context.Set<Roles>().Where(e => e.Id == id).Select(x => x.Name).AsNoTracking();

            if (await query.AnyAsync())
                return await query.FirstOrDefaultAsync();

            return null;
        }

        public async Task<string> GetNameByEmployerAsync(int employerId)
        {
            var query = _context.Set<Roles>().Join(_context.Set<Employer>(), role => role.Id,employer => employerId, (role, employer) => role.Name).AsNoTracking();

            if (await query.AnyAsync())
                return await query.FirstOrDefaultAsync();

            return null;
        }
    }
}