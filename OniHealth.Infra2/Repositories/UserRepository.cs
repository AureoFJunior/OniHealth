using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OniHealth.Domain;
using OniHealth.Domain.Models;
using OniHealth.Infra.Context;

namespace OniHealth.Infra.Repositories
{
    public class UserRepository : Repository<User>
    {
        private readonly UnitOfWork unitOfWork;
        public UserRepository(AppDbContext context) : base(context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public async override Task<User> GetByIdAsync(int id)
        {
            try
            {
                var query = _context.Set<User>().Where(e => e.Id == id).AsNoTracking();

                if (await query.AnyAsync())
                    return await query.FirstOrDefaultAsync();

                throw new NotFoundDatabaseException();
            }
            catch (Exception ex) { throw; }
        }

        public async override Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                var query = _context.Set<User>().AsNoTracking();

                if (await query.AnyAsync())
                    return await query.ToListAsync();

                throw new NotFoundDatabaseException();
            }
            catch (Exception ex) { throw; }
        }
    }
}