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
            modelBuilder.Entity<Exam>()
              .HasOne(e => e.ExamTime)
              .WithMany()
              .HasForeignKey(et => et.ExamTimeId)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Exam>()
            .HasOne(e => e.ExamPreparation)
            .WithMany()
            .HasForeignKey(et => et.ExamPreparationId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Exam>()
            .HasOne(e => e.Laboratory)
            .WithMany()
            .HasForeignKey(et => et.LaboratoryId)
            .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }

        #region DbSets
        public DbSet<Employer> Employer { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Consult> Consult { get; set; }
        public DbSet<ConsultType> ConsultType { get; set; }
        public DbSet<ConsultTime> ConsultTime { get; set; }
        public DbSet<Exam> Exam { get; set; }
        public DbSet<ExamTime> ExamTime { get; set; }
        public DbSet<ExamPreparation> ExamPreparation { get; set; }
        public DbSet<Laboratory> Laboratory { get; set; }
        #endregion
    }
}