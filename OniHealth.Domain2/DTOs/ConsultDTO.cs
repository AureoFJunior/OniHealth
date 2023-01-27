using OniHealth.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace OniHealth.Domain.DTOs
{
    public class ConsultDTO : BaseEntity
    {

        [Required]
        public string Title { get; set; }

        public int? ConsultTimeId { get; set; }
        public int? ConsultTypeId { get; set; }
        public int? CustomerId { get; set; }
        public int? DoctorId { get; set; }
        public int? ExamId { get; set; }
        public bool? CustomerIsPresent { get; set; }
        public bool? DoctorIsPresent { get; set; }
        public bool IsActive { get; set; }
    }
}