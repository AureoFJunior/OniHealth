using System.Collections.Generic;
using System.Linq;
using OniHealth.Domain.Models;
using OniHealth.Infra.Context;

namespace OniHealth.Infra.Repositories
{
    public class EmployerRepository : Repository<Employer>
    {
        public EmployerRepository(AppDbContext context) : base(context)
        { }

        public override Employer GetById(int id)
        {
            var query = _context.Set<Employer>().Where(e => e.Id == id);

            if (query.Any())
                return query.First();

            return null;
        }

        public override IEnumerable<Employer> GetAll()
        {
            var query = _context.Set<Employer>();

            return query.Any() ? query.ToList() : new List<Employer>();
        }
    }
}