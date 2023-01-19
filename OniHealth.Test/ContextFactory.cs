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
           .UseNpgsql("Server=onihealthdb.ce6vmgidwgna.us-east-1.rds.amazonaws.com;Database=onihealthDB;Uid=postgres;Pwd=postgres1234")
           .Options;

            // Insert seed data into the database using one instance of the context
            Context = new AppDbContext(options);
        }
    }
}