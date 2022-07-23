using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
            var query = _context.Set<User>().Where(e => e.Id == id);

            if (await query.AnyAsync())
                return await query.FirstOrDefaultAsync();

            return null;
        }

        public async override Task<IEnumerable<User>> GetAllAsync()
        {
            var query = _context.Set<User>();

            return await query.AnyAsync() ? await query.ToListAsync() : new List<User>();
        }
    }
}