using OniHealth.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace OniHealth.Infra.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Consult
            modelBuilder.Entity<Consult>()
                .HasOne(c => c.ConsultType)
                .WithMany()
                .HasForeignKey(c => c.ConsultTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Consult>()
                .HasOne(c => c.ConsultTime)
                .WithMany()
                .HasForeignKey(c => c.ConsultTimeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Consult>()
                .HasOne(c => c.Customer)
                .WithMany()
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Consult>()
                .HasOne(c => c.Employer)
                .WithMany()
                .HasForeignKey(c => c.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Consult>()
                .HasOne(c => c.Exam)
                .WithMany()
                .HasForeignKey(c => c.ExamId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Exam
          //  modelBuilder.Entity<Exam>()
          //.HasOne(e => e.ExamTime)
          //.WithOne(et => et.Exam)
          //.HasForeignKey<ExamTime>(et => et.ExamId);

          //  modelBuilder.Entity<Exam>()
          //      .HasOne(e => e.ExamPreparation)
          //      .WithOne(ep => ep.Exam)
          //      .HasForeignKey<ExamPreparation>(ep => ep.ExamId);

          //  modelBuilder.Entity<Exam>()
          //      .HasOne(e => e.Laboratory)
          //      .WithOne(l => l.Exam)
          //      .HasForeignKey<Laboratory>(l => l.ExamId);
            #endregion
        }

        public DbSet<Employer> Employer { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Consult> Consults { get; set; }
        public DbSet<ConsultType> ConsultTypes { get; set; }
        public DbSet<ConsultTime> ConsultTimes { get; set; }
        public DbSet<Exam> Exams { get; set; }
        //public DbSet<ExamTime> ExamTimes { get; set; }
        //public DbSet<ExamPreparation> ExamPreparations { get; set; }
        //public DbSet<Laboratory> Laboratories { get; set; }
    }
}