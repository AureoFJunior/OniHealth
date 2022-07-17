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
    }
}