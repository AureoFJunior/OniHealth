using Microsoft.EntityFrameworkCore;
using OniHealth.Infra.Context;

namespace OniHealth.Test
{
    public class ContextFactory
    {
        public readonly AppDbContext Context;

        public ContextFactory()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
           .UseSqlServer("Server=localhost\\SQLEXPRESS;Database=oniHealth;Trusted_Connection=True;")
           .Options;

            // Insert seed data into the database using one instance of the context
            Context = new AppDbContext(options);
        }
    }
}