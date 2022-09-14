using OniHealth.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace OniHealth.Infra.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employer> Employer { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<User> User { get; set; }
    }
}