using OniHealth.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OniHealth.Infra.Context
{
    public class AppDbContextIndex
    {
        public static void Configure(ModelBuilder builder)
        {
            #region Consult
            builder.Entity<Consult>()
                .HasIndex(x => x.Id);
            builder.Entity<Consult>()
                .HasIndex(x => x.ConsultTimeId);
            builder.Entity<Consult>()
                .HasIndex(x => x.ConsultTypeId);
            builder.Entity<Consult>()
                .HasIndex(x => x.ExamId);
            builder.Entity<Consult>()
               .HasIndex(x => x.DoctorId);
            builder.Entity<Consult>()
               .HasIndex(x => x.CustomerId);
            builder.Entity<Consult>()
               .HasIndex(x => x.Title);
            #endregion

            #region Exam
            builder.Entity<Exam>()
               .HasIndex(x => x.Id);
            builder.Entity<Exam>()
               .HasIndex(x => x.ExamTimeId);
            builder.Entity<Exam>()
               .HasIndex(x => x.ExamPreparationId);
            builder.Entity<Exam>()
               .HasIndex(x => x.LaboratoryId);
            builder.Entity<Exam>()
               .HasIndex(x => x.Name);
            #endregion
        }
    }
}