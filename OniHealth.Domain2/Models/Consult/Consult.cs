using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using OniHealth.Domain.Enums;

namespace OniHealth.Domain.Models
{
    public class Consult : BaseEntity
    {
        public Consult() { }

        public Consult(string title, bool isActive, int? consultTypeId, int? consultTimeId, int? customerId, int? doctorId, int? examId, bool? customerIsPresent, bool? doctorIsPresent)
        {
            ValidateCategory(title);
            Title = title;
            isActive = isActive;
            ConsultTypeId = consultTypeId;
            ConsultTimeId = consultTimeId;
            CustomerId = customerId;
            DoctorId = doctorId;
            ExamId = examId;
            CustomerIsPresent = customerIsPresent;
            DoctorIsPresent = doctorIsPresent;
        }

        #region Fields
        public string Title { get; set; }

        [ForeignKey(nameof(ConsultTime))]
        public int? ConsultTimeId { get; set; }
        public virtual ConsultTime ConsultTime { get; set; }

        [ForeignKey(nameof(ConsultType))]
        public int? ConsultTypeId { get; set; } 
        public virtual ConsultType ConsultType { get; set; }

        [ForeignKey(nameof(Customer))]
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [ForeignKey(nameof(Employer))]
        public int? DoctorId { get; set; }
        public virtual Employer Employer { get; set; }

        [ForeignKey(nameof(Exam))]
        public int? ExamId { get; set; }
        public virtual Exam Exam { get; set; }

        public bool? CustomerIsPresent { get; set; }
        public bool? DoctorIsPresent { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; }
        #endregion

        public void Update(string title)
        {
            ValidateCategory(title);
        }
        private void ValidateCategory(string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new InvalidOperationException("The consult's title is invalid");
        }
    }
}